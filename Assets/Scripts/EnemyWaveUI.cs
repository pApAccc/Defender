using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class EnemyWaveUI : MonoBehaviour
    {
        TextMeshProUGUI enemyWaveText;
        TextMeshProUGUI enemyMessageText;
        RectTransform enemyWaveSpawnPositionIndicator;
        RectTransform enemyClosestIndicator;
        Camera mainCamera;

        [SerializeField] EnemyWaveManager enemyWaveManager;

        private void Awake()
        {
            enemyWaveText = transform.Find("EnemyWaveText").GetComponent<TextMeshProUGUI>();
            enemyMessageText = transform.Find("EnemyMessageText").GetComponent<TextMeshProUGUI>();
            enemyWaveSpawnPositionIndicator = transform.Find("EnemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
            enemyClosestIndicator = transform.Find("EnemyClosestIndicator").GetComponent<RectTransform>();
            SetWaveText("Wave " + enemyWaveManager.GetWaveNumber());
        }
        private void Start()
        {
            mainCamera = Camera.main;
            enemyWaveManager.OnWaveNumberChanged += (object sender, EventArgs e) =>
            {
                SetWaveText("Wave " + enemyWaveManager.GetWaveNumber());
            };
        }
        private void Update()
        {

            HandleNextWaveMessage();
            HandleEnemyWaveIndicator();
            HandleClosestEnemyIndicator();

        }
        private void HandleNextWaveMessage()
        {
            float nextWaveTimer = enemyWaveManager.GetNextWaveTimer();
            if (nextWaveTimer <= 0)
            {
                SetMessageText("");
            }
            else
            {
                SetMessageText("Next Wave In : " + nextWaveTimer.ToString("F1") + "s");
            }
        }

        private void HandleEnemyWaveIndicator()
        {
            //更改敌人诞生点图标
            Vector3 dirToNextSpawnPos = (enemyWaveManager.GetWavePosition() - mainCamera.transform.position).normalized;

            enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPos * 300;
            enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAnglefromVector(dirToNextSpawnPos));

            float disToNextSpawnPoint = Vector3.Distance(enemyWaveManager.GetWavePosition(), mainCamera.transform.position);
            enemyWaveSpawnPositionIndicator.gameObject.SetActive(disToNextSpawnPoint > mainCamera.orthographicSize * 1.5);



        }

        private void HandleClosestEnemyIndicator()
        {
            float targetMaxRadius = 9999999f;
            Collider2D[] EnemyArray = Physics2D.OverlapCircleAll(mainCamera.transform.position, targetMaxRadius);
            Enemy targetEnemy = null;
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

            //更改最近敌人图标
            if (targetEnemy != null)
            {
                Vector3 dirToClostEnemyPos = (targetEnemy.transform.position - mainCamera.transform.position).normalized;

                enemyClosestIndicator.anchoredPosition = dirToClostEnemyPos * 250;
                enemyClosestIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAnglefromVector(dirToClostEnemyPos));

                float disToClostEnemyPoint = Vector3.Distance(targetEnemy.transform.position, mainCamera.transform.position);
                enemyClosestIndicator.gameObject.SetActive(disToClostEnemyPoint > mainCamera.orthographicSize * 1.5);
            }
            else
            {
                //不存在敌人
                enemyClosestIndicator.gameObject.SetActive(false);

            }
        }
        void SetWaveText(string waveText)
        {
            enemyWaveText.text = waveText;
        }

        void SetMessageText(string messageText)
        {
            enemyMessageText.text = messageText;
        }
    }
}
