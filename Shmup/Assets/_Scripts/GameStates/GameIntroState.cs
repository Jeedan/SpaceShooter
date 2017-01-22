using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntroState : IState
{
    private GameManager gm;

    public GameIntroState()
    {

    }

    public GameIntroState(GameObject obj)
    {
        gm = obj.GetComponent<GameManager>();
    }

    public void OnEnter()
    {
        Debug.Log("intro state OnEnter");
    }

    public void OnExit()
    {
        Debug.Log("intro state OnExit");
    }

    public void OnUpdate()
    {
        if (Input.anyKeyDown)
        {
            gm.ChangeState(GameStateID.GAME_MAIN_MENU_STATE);
        }
    }
}
