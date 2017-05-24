using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainMenuState : IState
{

    GameManager gm;

    Button playGame;

    public GameMainMenuState()
    {

    }

    public GameMainMenuState(GameObject obj)
    {
        gm = obj.GetComponent<GameManager>();


    }

    public void OnEnter()
    {
        gm.UI_MainMenu_RootOBJ.SetActive(true);
        playGame = gm.UI_MainMenu_RootOBJ.GetComponentInChildren<Button>();
        playGame.onClick.AddListener(PlayGame);
    }

    public void OnExit()
    {
        gm.UI_MainMenu_RootOBJ.SetActive(false);

    }

    public void OnUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PlayGame();
        }
    }

    void PlayGame()
    {
        gm.ChangeState(GameStateID.SHIP_TUTORIAL_STATE);
    }
}
