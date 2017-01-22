using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainMenuState : IState
{

    GameManager gm;

    public GameMainMenuState()
    {

    }

    public GameMainMenuState(GameObject obj)
    {
        gm = obj.GetComponent<GameManager>();
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            gm.ChangeState(GameStateID.SHIP_TUTORIAL_STATE);
        }
    }
}
