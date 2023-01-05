using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 
/// </summary>
namespace ns
{
    public class EnemyWaveManager : MonoBehaviour
    {
        public event EventHandler OnWaveNumberChanged;
        enum State
        {
            WaitForNextWave,
            SpwaningWave
        }
        State state;

        float nextWaveTimer;
        float nextEnemySpawnTimer;

        int spawnEnemyAmount;
        int waveNumber = 0;

        [SerializeField] List<Transform> EnemySpawnPositonList;
        [SerializeField] Transform enemyComingIcon;
        Vector3 spawnPosition;

        private void Start()
        {
            state = State.WaitForNextWave;
            spawnPosition = EnemySpawnPositonList[Random.Range(0, EnemySpawnPositonList.Count)].position;
            enemyComingIcon.position = spawnPosition;
            SpawnEnemy();
        }
        private void Update()
        {
            switch (state)
            {
                //在等待下一波状态下
                case State.WaitForNextWave:
                    nextWaveTimer -= Time.deltaTime;
                    if (nextWaveTimer <= 0)
                    {
                        SpawnEnemy();
                    }
                    break;

                //在诞生敌人状态下
                case State.SpwaningWave:
                    if (spawnEnemyAmount > 0)
                    {
                        nextEnemySpawnTimer -= Time.deltaTime;
                        if (nextEnemySpawnTimer <= 0)
                        {
                            Enemy.CreateEnemy(spawnPosition + UtilsClass.GetRandomDir() * Random.Range(0f, 10f));
                            nextEnemySpawnTimer = Random.Range(0f, .2f);
                            spawnEnemyAmount--;

                            if (spawnEnemyAmount <= 0f)
                            {
                                state = State.WaitForNextWave;
                                spawnPosition = EnemySpawnPositonList[Random.Range(0, EnemySpawnPositonList.Count)].position;
                                enemyComingIcon.position = spawnPosition;
                                nextWaveTimer = 10f;
                            }
                        }
                    }
                    break;
            }
        }
        //诞生敌人波次
        void SpawnEnemy()
        {
            spawnEnemyAmount = 5 + 3 * waveNumber;
            state = State.SpwaningWave;
            waveNumber++;
            OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
        }

        public int GetWaveNumber()
        {
            return waveNumber;
        }

        public float GetNextWaveTimer()
        {
            return nextWaveTimer;
        }

        public Vector3 GetWavePosition()
        {
            return spawnPosition;
        }
    }
}
