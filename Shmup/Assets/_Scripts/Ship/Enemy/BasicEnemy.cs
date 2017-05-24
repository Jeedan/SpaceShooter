using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{

    public enum MovementType
    {
        LINEAR,
        SINEWAVE
    };

    public float currentHealth = 3.0f;
    public float speed = 3.0f;
    public int spawnIndex = 0; // index of instantiation, this is used for sine wave offset in movement 
    public GameObject gibsPrefab;
    public MovementType movementType = MovementType.LINEAR;

    public delegate void OnDeath();
    public event OnDeath OnEnemyDeath;

    public SineMovement SineMover { get { return sineMover; } private set { sineMover = value; } }

    private LinearMovement linearMover;
    private SineMovement sineMover;



    private float maxHealth;
    private GameManager gm;

    void Awake()
    {
        linearMover = new LinearMovement();
        sineMover = new SineMovement();
    }

    // Use this for initialization
    void Start()
    {
        maxHealth = currentHealth;
        gm = FindObjectOfType<GameManager>();
        gm.enemiesInScene++;
        sineMover.waveOffset = (1 - spawnIndex) * 2; // this works but who knows why. This is used for sine wave offset positioning
    }

    // Update is called once per frame
    void Update()
    {
        if (movementType == MovementType.SINEWAVE)
        {
            transform.position = sineMover.Move(transform.position, transform.right, speed);
        }
        else
        {
            transform.position = linearMover.Move(transform.position, transform.right, -speed);
        }

        KillOnScreenExit();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0.0f)
        {
            ExplodeGibs();

            if (OnEnemyDeath != null)
            {
                OnEnemyDeath();
            }

            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);

        gm.enemiesInScene--;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void ExplodeGibs()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(gibsPrefab, transform.position, transform.rotation);
            //Rigidbody2D gibRig = gib.GetComponent<Rigidbody2D>();
            //gibRig.AddForce(Random.insideUnitCircle * 200.0f);

        }
    }

    public void KillOnScreenExit()
    {
        float screenX = Camera.main.WorldToScreenPoint(transform.position).x + 1.0f;
        if (screenX < Camera.main.transform.position.x)
        {
            //Debug.Log("i'm to the left of the screen");
            Die();
        }
    }
}
