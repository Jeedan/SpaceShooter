using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTutorialState : IState
{

    GameManager gm;

    public int killsToSpawnNewWave = 2;
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
        currentKillCount = gm.enemyShipKills;
        if(currentKillCount % 3 == 0 && currentKillCount != 0)
        {
            killsToSpawnNewWave = gm.enemyShipKills + killsToSpawnNewWave;
            currentKillCount = 0;
            gm.StartCoroutine(NewWaveOfEnemies());
            Debug.Log("Total kills needed " + killsToSpawnNewWave);
        }
    }

    IEnumerator NewWaveOfEnemies()
    {
        gm.enemySpawner.SpawnWave();
        yield return null;
    }
}

