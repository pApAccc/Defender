using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class Enemy : MonoBehaviour
    {
        Transform targetTF;
        Rigidbody2D rb2d;
        float lookForTargetTimer;
        float lookForTargetTimerMax = .2f;

        public static Enemy CreateEnemy(Vector3 position)
        {
            Transform pfEnemy = Resources.Load<Transform>("pfenemy");
            Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);

            Enemy enemy = enemyTransform.GetComponent<Enemy>();
            return enemy;
        }
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();

            lookForTargetTimer = UnityEngine.Random.Range(0, lookForTargetTimerMax);
        }
        private void Start()
        {
            targetTF = BuilderManager.Instance.GethqBuilding()?.transform;

            GetComponent<HealthSystem>().OnDead += (object sender, EventArgs e) =>
            {
                Destroy(gameObject);
            };
        }

        private void Update()
        {
            HandleTrageting();
            HandleMove();

        }

        private void HandleMove()
        {
            //向目标移动
            if (targetTF != null)
            {
                Vector3 movedir = (targetTF.position - transform.position).normalized;
                float moveSpeed = 7;
                rb2d.velocity = movedir * moveSpeed;
            }
            //如果范围内不存在建筑并且hq已销毁
            else
            {
                rb2d.velocity = new Vector2(0, 0);
            }

        }

        private void HandleTrageting()
        {
            //每过一定时间查找最近的目标
            lookForTargetTimer -= Time.deltaTime;
            if (lookForTargetTimer < 0)
            {
                lookForTargetTimer = lookForTargetTimerMax;
                LookForTarget();
            }
        }

        //碰撞受伤方法
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Building building = collision.gameObject.GetComponent<Building>();
            if (building != null)
            {
                HealthSystem healthSystem = building.GetComponent<HealthSystem>();
                healthSystem.Damage(10);

                Destroy(gameObject);
            }
        }

        //索敌方法
        private void LookForTarget()
        {
            float targetMaxRadius = 10f;
            Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

            //范围内存在目标，将目标设为最近的建筑
            foreach (Collider2D collider2D in collider2DArray)
            {
                Building building = collider2D.GetComponent<Building>();
                if (building != null)
                {
                    if (targetTF == null)
                    {
                        targetTF = building.transform;
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, building.transform.position) <
                            Vector3.Distance(transform.position, targetTF.transform.position))
                        {
                            targetTF = building.transform;
                        }
                    }
                }
            }

            //范围内找不到目标，将目标设为hq
            if (targetTF == null)
            {
                targetTF = BuilderManager.Instance.GethqBuilding()?.transform;
            }


        }

    }
}
