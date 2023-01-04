using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class Tower : MonoBehaviour
    {
        Enemy targetEnemy;
        Transform arrowSpawnPoint;
        float lookForTargetTimer;
        float lookForTargetTimerMax = .2f;

        float shootTimer;
        [SerializeField] float shootTimerMax = .3f;
        private void Awake()
        {
            arrowSpawnPoint = transform.Find("ArrowSpawnPoint").transform;
        }
        private void Update()
        {
            HandleTrageting();
            HandleShotting();

        }

        private void HandleShotting()
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer < 0)
            {
                shootTimer = shootTimerMax;

                if (targetEnemy != null)
                {
                    ArrowProject.CreateEnemy(arrowSpawnPoint.position, targetEnemy);
                }
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
        private void LookForTarget()
        {
            float targetMaxRadius = 20f;
            Collider2D[] EnemyArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

            //范围内存在目标，将目标设为最近的建筑
            foreach (Collider2D collider2D in EnemyArray)
            {
                Enemy enemy = collider2D.GetComponent<Enemy>();
                if (enemy != null)
                {
                    if (targetEnemy == null)
                    {
                        targetEnemy = enemy;
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, enemy.transform.position) <
                            Vector3.Distance(transform.position, targetEnemy.transform.position))
                        {
                            targetEnemy = enemy;
                        }
                    }
                }
            }
        }
    }
}
