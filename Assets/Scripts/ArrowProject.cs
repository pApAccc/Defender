using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class ArrowProject : MonoBehaviour
    {
        Transform targetTF;
        [SerializeField] float moveSpeed = 20;

        public static ArrowProject CreateEnemy(Vector3 position, Enemy enemy)
        {
            Transform pfArrowProject = Resources.Load<Transform>("pfArrowProject");
            Transform arrowTransform = Instantiate(pfArrowProject, position, Quaternion.identity);

            ArrowProject arrow = arrowTransform.GetComponent<ArrowProject>();
            arrow.SetTarget(enemy.transform);
            return arrow;
        }

        Vector3 lastMoveDir;
        float time2Die = 2;
        private void Update()
        {
            Vector3 moveDir;
            if (targetTF != null)
            {
                moveDir = (targetTF.position - transform.position).normalized;
                lastMoveDir = moveDir;
            }
            else
            {
                moveDir = lastMoveDir;
            }


            transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAnglefromVector(moveDir));
            transform.position += moveDir * Time.deltaTime * moveSpeed;

            time2Die -= Time.deltaTime;
            if (time2Die < 0)
            {
                Destroy(gameObject);
            }
        }

        private void SetTarget(Transform targetTF)
        {
            this.targetTF = targetTF;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                int damageAmount = 10;
                enemy.GetComponent<HealthSystem>().Damage(damageAmount);

                Destroy(gameObject);
            }
        }
    }
}
