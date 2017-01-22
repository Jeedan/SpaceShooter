using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLinear : MonoBehaviour
{
    public float speed = 2.0f;
    public float aliveTime = 2.0f;

    LinearMovement linearMove;

    private void Start()
    {
        linearMove = new LinearMovement();

    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Destroy(gameObject, aliveTime);
    }

    public void Move()
    {
        transform.position = linearMove.Move(transform.position, transform.right, speed); 
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        BasicEnemy enemyCollision = collision.GetComponent<BasicEnemy>();

        enemyCollision.TakeDamage(1.0f);
        Destroy(gameObject);
    }
}
