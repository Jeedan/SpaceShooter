using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLinear : MonoBehaviour
{
    public float speed = 2.0f;
    public float aliveTime = 2.0f;

    LinearMovement linearMove;
    public GameObject owner;
    public Vector3 direction = Vector3.zero;
    public Transform target;


    private void Start()
    {
        linearMove = new LinearMovement();

       // direction = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Destroy(gameObject, aliveTime);
    }

    public void Move()
    {
        if (target)
        {
            transform.position = linearMove.Move(transform.position, direction, speed);

        }else
        {
            transform.position = linearMove.Move(transform.position, transform.right, speed);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        ShipController playerShip = owner.GetComponent<ShipController>();

        ShipController playerCollision = collision.GetComponent<ShipController>();

        BasicEnemy enemyCollision = collision.GetComponent<BasicEnemy>();

        if(enemyCollision != null)
        {
            enemyCollision.OnEnemyDeath += playerShip.OnEnemyDeath;
            enemyCollision.TakeDamage(1.0f);
        }

        ServitorBoss servitor = collision.GetComponent<ServitorBoss>();
        if (servitor != null)
        {
            playerShip = FindObjectOfType<ShipController>();
            servitor.OnEnemyDeath += playerShip.OnEnemyDeath;
            servitor.TakeDamage(1.0f);
        }

        if (playerCollision != null)
        {
            playerShip = collision.GetComponent<ShipController>();
            playerShip.TakeDamage(5.0f);
        }

        Destroy(gameObject);
    }
}
