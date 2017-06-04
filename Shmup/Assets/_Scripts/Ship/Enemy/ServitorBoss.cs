using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServitorBoss : MonoBehaviour
{
    public float currentHealth;
    public GameObject gibsPrefab;


    public float speed = 10.0f;

    private GameManager gm;
    private float maxHealth;

    public float MaxHealth { get { return maxHealth; } private set { maxHealth = value; } }
    public float HealthPercentage { get { return currentHealth / maxHealth; } private set { maxHealth = value; } }


    public delegate void OnDeath();
    public event OnDeath OnEnemyDeath;

    // TODO boss weapon refactor into generic weapon class
    public GameObject bulletPrefab;
    public float damage = 1.0f;
    public float fireRate = 0.3f;
    public float nextFire;

    // todo refactor into better state pattern
    public enum BossPattern
    {
        THINKING,
        MOVE_CENTER,
        MOVE_UP,
        MOVE_DOWN,
        FIRE
    };

    private string[] bossStates = new string[] { "ServitorThinkingState", "ServitorMoveCenterState", "ServitorMoveUpState", "ServitorMoveDownState", "ServitorFireState" };

    private StateMachine bossPatternMachine;


    public int currPos = 0;

    private ShipController playerShip;
    public ShipController PlayerShip { get { return playerShip; } private set { playerShip = value; } }

    private bool exploding = false;
    private bool isDoneExploding = false;

    // Use this for initialization
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        playerShip = FindObjectOfType<ShipController>().GetComponent<ShipController>();
        
        maxHealth = currentHealth;

        bossPatternMachine = new StateMachine();

        IState servitorThinkingState = new ServitorThinkingState(this);
        IState servitorMoveUpState = new ServitorMoveUpState(this);
        IState servitorMoveDownState = new ServitorMoveDownState(this);
        IState servitorMoveCenter = new ServitorMoveCenterState(this);

        IState servitorFireState = new ServitorFireState(this);


        AddState(BossPattern.THINKING, servitorThinkingState);
        AddState(BossPattern.MOVE_UP, servitorMoveUpState);
        AddState(BossPattern.MOVE_DOWN, servitorMoveDownState);
        AddState(BossPattern.MOVE_CENTER, servitorMoveCenter);
        AddState(BossPattern.FIRE, servitorFireState);

        ChangeState(BossPattern.THINKING);
    }

    // Update is called once per frame
    void Update()
    {
        bossPatternMachine.OnUpdate();
    }


    public void AddState(BossPattern _stateID, IState _state)
    {
        bossPatternMachine.AddState(bossStates[(int)_stateID], _state);
    }

    public void ChangeState(BossPattern _stateID)
    {
        ChangeState(bossStates[(int)_stateID]);
    }


    public void ChangeState(string _state)
    {
        bossPatternMachine.ChangeState(_state);
    }

    public IEnumerator MoveServitor(float screenPositionPercent)
    {
        bool reached = false;

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.90f, Screen.height * screenPositionPercent, 1.0f));
        Vector3 direction = (targetPos - transform.position).normalized;
        //Debug.Log(direction);
        Vector3 move = direction * speed * Time.deltaTime;

        while (!reached)
        {
            transform.position += move;

            if (Vector3.Distance(targetPos, transform.position) <= 0.2f)
            {
                move = Vector3.zero;
                Vector3 currPos = transform.position;
                transform.position = Vector3.Lerp(currPos, targetPos, Time.deltaTime * (speed * 0.5f));

            //    Debug.Log("reached true");
                reached = true;
            }


            yield return null;

        }

        yield return new WaitForSeconds(2);
     //   Debug.Log("reached end of MoveServitor");
        ChangeState(BossPattern.FIRE);
    }

    public void TakeDamage(float amount)
    {
        if (!exploding)
        {
            currentHealth -= amount;
        }


        if (currentHealth <= 0.0f)
        {
            //ExplodeGibs();
            if (!exploding)
            {
                StartCoroutine(ExplodeGibsWithDelay());
            }

            if (OnEnemyDeath != null)
            {
                OnEnemyDeath();
            }
            Die();
        }
    }

    public void Die()
    {
        if (isDoneExploding)
        {
            Destroy(gameObject);
        }

        gm.enemiesInScene--;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void RotateTowardsTarget()
    {
        if (PlayerShip != null)
        {
            if (HealthPercentage <= 0.5f)
            {
                Vector3 direction = PlayerShip.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            }
        }
    }

    public void ExplodeGibs()
    {
        for (int j = 0; j < 4; j++)
        {

            Vector3 randomPos = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y) + transform.position;
            for (int i = 0; i < 10; i++)
            {
                Instantiate(gibsPrefab, randomPos, transform.rotation);
                //Rigidbody2D gibRig = gib.GetComponent<Rigidbody2D>();
                //gibRig.AddForce(Random.insideUnitCircle * 200.0f);

            }
        }
    }


    IEnumerator ExplodeGibsWithDelay()
    {
        exploding = true;
        for (int j = 0; j < 4; j++)
        {
            Vector3 randomPos = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y) + transform.position;
         //   Debug.Log("exploding " + j);
            for (int i = 0; i < 10; i++)
            {
                Instantiate(gibsPrefab, randomPos, transform.rotation);
            }
            yield return new WaitForSeconds(0.2f);
        }

        isDoneExploding = true;
    }


}
