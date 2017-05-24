using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateID
{
    GAME_INTRO_STATE = 0,
    GAME_MAIN_MENU_STATE = 1,
    SHIP_TUTORIAL_STATE = 2
};

public class GameManager : MonoBehaviour
{
    private StateMachine gameStateMachine;

    // String Array of States for the game
    private string[] gameStates = new string[] { "GameIntroState",
                                                 "GameMainMenuState",
                                                 "ShipTutorialState" };

    public EnemySpawner enemySpawner;

    public ShipController playerShip;

    public int enemiesInScene = 0;


    public GameObject UI_Intro_RootOBJ;
    public GameObject UI_MainMenu_RootOBJ;
    public GameObject UI_EnemiesKilled_RootOBJ;

    // tutorial level
    // if we want to tweak settings in the Unity inspector
    // set the level as a Serializable field
    // however we will need to specify exactly what class the state object is when creating it
    // instead of using an IState interface
    // because unity does not show interfaces in the inspector
    [SerializeField]
    private ShipTutorialState shipTutorialState;

    private void InitializeStates()
    {
        IState gameIntroState = new GameIntroState(gameObject);
        IState gameMainMenuState = new GameMainMenuState(gameObject);

        // tutorial level
        //  IState shipTutorialState = new ShipTutorialState(gameObject);
        shipTutorialState = new ShipTutorialState(gameObject);

        // add the states to the statemachine 
        AddState(GameStateID.GAME_INTRO_STATE, gameIntroState);
        AddState(GameStateID.GAME_MAIN_MENU_STATE, gameMainMenuState);

        AddState(GameStateID.SHIP_TUTORIAL_STATE, shipTutorialState);

        ChangeState(GameStateID.GAME_INTRO_STATE);
    }

    private void Awake()
    {
        gameStateMachine = new StateMachine();

        enemySpawner = GetComponent<EnemySpawner>();

        if (!UI_Intro_RootOBJ.activeInHierarchy)
        {
            UI_Intro_RootOBJ.SetActive(true);
        }

        UI_MainMenu_RootOBJ.SetActive(false);
        UI_EnemiesKilled_RootOBJ.SetActive(false);

    }

    // Use this for initialization
    void Start()
    {
        InitializeStates();
    }

    // Update is called once per frame
    void Update()
    {
        gameStateMachine.OnUpdate();
    }

    public void ChangeState(GameStateID _stateID)
    {
        ChangeState(gameStates[(int)_stateID]);
    }

    public void ChangeState(string _state)
    {
        gameStateMachine.ChangeState(_state);
    }

    public void AddState(GameStateID _stateID, IState _state)
    {
        gameStateMachine.AddState(gameStates[(int)_stateID], _state);
    }

    public void AddState(string _name, IState _state)
    {
        gameStateMachine.AddState(_name, _state);
    }

    public void PopState(string key)
    {
        gameStateMachine.PopState(key);
    }

    public void SpawnNewEnemyWave()
    {
        enemySpawner.SpawnWave();
    }

    public void InstantiatePlayerShip()
    {
        //ShipController ship = Instantiate(playerShip);
        Instantiate(playerShip);
    }
}
