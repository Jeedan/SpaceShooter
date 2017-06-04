using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShipTutorialState : IState
{

    GameManager gm;
    EnemySpawner enemySpawner;
    ShipController playerShip;

    public int killsToSpawnNewWave = 3;

    private int totalKillCount = 0;
    //private bool firstWave = true;

    public ShipTutorialState()
    {

    }

    public ShipTutorialState(GameObject obj)
    {
        gm = obj.GetComponent<GameManager>();
        enemySpawner = gm.enemySpawner;
    }

    public void OnEnter()
    {
        // TODO
        // spawn player
        // spawn enemy waves
        gm.InstantiatePlayerShip();

        //enemySpawner.SpawnWave();
        enemySpawner.SpawnBoss();

        playerShip = Object.FindObjectOfType<ShipController>().GetComponent<ShipController>();


        //Debug.Log("kills needed " + killsToSpawnNewWave  + " total kills: " + totalKillCount + " wave " + enemySpawner.currentWave);
    }

    public void OnExit()
    {
        //TODO change state to Ground mission tutorial...
        gm.UI_EnemiesKilled_RootOBJ.SetActive(false);

    }

    public void OnUpdate()
    {
        if(enemySpawner.currentWave >= enemySpawner.waves.Length && gm.enemiesInScene <= 0)
        {
            DisplayGameOver();
        }
    }

    public void DisplayGameOver()
    {
        totalKillCount = playerShip.totalKillCount;
        // we did not kill enough enemies to spawn next wave
        gm.UI_EnemiesKilled_RootOBJ.SetActive(true);
        Text enemiesKilledTxt = gm.UI_EnemiesKilled_RootOBJ.GetComponentInChildren<Text>();
        enemiesKilledTxt.text = "You killed " + totalKillCount + " enemies!"
            + "\nYou survived for " + enemySpawner.currentWave + " waves!";
    }

}

