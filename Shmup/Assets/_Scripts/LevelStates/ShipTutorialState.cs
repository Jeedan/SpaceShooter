using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTutorialState : IState
{

    GameManager gm;

    public int killsToSpawnNewWave = 3;
    private int currentKillCount = 0;

    public ShipTutorialState()
    {

    }

    public ShipTutorialState(GameObject obj)
    {
        gm = obj.GetComponent<GameManager>();
    }

    public void OnEnter()
    {
        // TODO
        // spawn player
        // spawn enemy waves
        gm.InstantiatePlayerShip();
        gm.enemySpawner.SpawnWave();
    }

    public void OnExit()
    {
        //TODO change state to Ground mission tutorial...
    }

    public void OnUpdate()
    {
        // TODO spawn a wave every third kill
        var totalKillCount = 0;
        totalKillCount += gm.enemyShipKills;
        currentKillCount = gm.enemyShipKills;
        if (currentKillCount % killsToSpawnNewWave == 0 && currentKillCount != 0)
        {
            Debug.Log("kills needed " + killsToSpawnNewWave + " currentKillCount: " + currentKillCount + " total kills: "+ totalKillCount);
            gm.enemyShipKills = 0;
            gm.StartCoroutine(NewWaveOfEnemies());
            //   killsToSpawnNewWave = gm.enemyShipKills + 2;
        }
    }

    IEnumerator NewWaveOfEnemies()
    {
        if (!gm.enemySpawner.isSpawning)
        {
            gm.enemySpawner.SpawnWave();
        }
        yield return null;
    }
}

