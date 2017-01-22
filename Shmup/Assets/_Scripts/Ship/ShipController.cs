using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float currentHealth = 30.0f;
    private float maxHealth;


    public float speed = 1.0f;
    public float friction = 0.5f;
    public float drag = 0.9f;
    public Vector2 minMaxAcceleration = new Vector3(1.0f, 10.0f);
    public float velocity = 0.0f;

    private float moveDir;
    private float acceleration = 0;
    private Vector3 dir = Vector3.zero;
    private Vector3 move = Vector3.zero;

    // TODO temp weapon 
    private BasicShipWeapon basicWeapon;

    // Use this for initialization
    void Start()
    {
        Vector3 shipSpawnPos = new Vector3(Screen.width * 0.1f, Screen.height * 0.5f, 1);
        transform.position = Camera.main.ScreenToWorldPoint(shipSpawnPos);

        basicWeapon = GetComponent<BasicShipWeapon>();

        maxHealth = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAcceleration();
        MoveShip();
        ClampAcceleration();
        HandleShooting();
        ClampInsideCamera();

    }

    private void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        basicWeapon.Shoot();
    }

    private void MoveShip()
    {
        velocity = 0.0f;
        dir = transform.up * moveDir;
        velocity += acceleration;
        move = dir * velocity;
        move *= friction;
        transform.position += move * Time.deltaTime;
    }

    private void HandleAcceleration()
    {
        float vertical = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(vertical) > 0.1f)
        {
            acceleration += speed * Time.deltaTime;
            moveDir = vertical;
        }
        else
        {
            if (acceleration > 0.0f)
            {
                acceleration *= drag;
            }
        }

        if (acceleration <= 0.01f)
        {
            moveDir = 0;
            acceleration = 0;
        }
    }

    private void ClampAcceleration()
    {
        if (acceleration >= minMaxAcceleration.y)
        {
            acceleration = minMaxAcceleration.y;
        }

        if (acceleration <= minMaxAcceleration.x)
        {
            acceleration = minMaxAcceleration.x;
        }
    }

    private void ClampInsideCamera()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
    
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        BasicEnemy enemyCol = collision.gameObject.GetComponent<BasicEnemy>();

        if (enemyCol != null)
        {
            TakeDamage(5.0f);
        }
    }
}
