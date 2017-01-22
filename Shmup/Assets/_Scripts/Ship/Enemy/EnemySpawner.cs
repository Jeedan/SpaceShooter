using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnDelay = 1.0f;

    public bool isSpawning = false;
    public int spawnAmount = 5;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    public void SpawnWave()
    {
        isSpawning = true;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(1.5f);
        while (isSpawning)
        {
            for (int i = 0; i <= spawnAmount; i++)
            {
                GameObject enemyGO = Instantiate(enemyPrefab);

                float randomSpawnHeight = Random.Range(0.05f, 0.85f);

                Vector3 spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 3.0f, Screen.height * randomSpawnHeight, 1.0f));

                enemyGO.transform.position = spawnPos;

                yield return new WaitForSeconds(spawnDelay);

                if(i == spawnAmount)
                {
                    isSpawning = false;
                }
            }
        }
    }
}
