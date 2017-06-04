using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject fodderEnemy;
    public GameObject sineWaveFodderEnemy;
    
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public float spawnDelay = 1.0f;
        public int spawnAmount = 5;
        public bool isSpawning = false;

        public float spawnHeight;
    }
    
    public Wave[] waves;

    // todo wave spawner 
    public float waveSpawnDelay = 3.0f;
    public int currentWave = 0;

    // TODO Refactor boss spawn
    public GameObject ServitorPrefab;

    public List<BasicEnemy> enemyList = new List<BasicEnemy>();
    
    public void SpawnWave()
    {
        //StartCoroutine(SpawnLinearWave());
        StartCoroutine(SpawnWaves());
        //StartCoroutine(SpawnSineWaveEnemies(3));
    }

    IEnumerator SpawnWaves()
    {
        for(int i = 0; i < waves.Length; i++)
        {
            currentWave++;
            waves[i].isSpawning = true;
            for(int j = 0; j < waves[i].spawnAmount; j++)
            {
                CreateEnemyAndAddToList(waves[i].spawnHeight, j, waves[i].enemyPrefab);
                yield return new WaitForSeconds(waves[i].spawnDelay);
            }

            waves[i].isSpawning = false;
            yield return new WaitForSeconds(waveSpawnDelay);
        }
    }
    
    public void SpawnBoss()
    {

        GameObject servitor = Instantiate(ServitorPrefab);
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.90f, Screen.height * 0.5f, 1.0f));

        servitor.transform.position = spawnPos;
    }

    void CreateEnemyAndAddToList(float randomSpawnHeight, int index, GameObject prefab)
    {
        GameObject enemyGO = Instantiate(prefab);
        BasicEnemy enemy = enemyGO.GetComponent<BasicEnemy>();
        enemy.spawnIndex = index;
        //enemy.SineMover.waveOffset =(1-i)* 2; // this works but who knows why 
        //enemy.waveOffset = (i+1) * 3.141f; // this works no idea why still a little out of sync

        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 3.0f, Screen.height * randomSpawnHeight, 1.0f));

        enemyGO.transform.position = spawnPos;

        enemyList.Add(enemy);
    }
}
