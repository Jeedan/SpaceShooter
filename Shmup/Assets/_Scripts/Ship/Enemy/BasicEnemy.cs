using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float currentHealth = 3.0f;
    public float speed = 3.0f;
    public float aliveTime = 7.0f;
    private float maxHealth;


    LinearMovement linearMover;

    // Use this for initialization
    void Start()
    {
        maxHealth = currentHealth;
        linearMover = new LinearMovement();
        Destroy(gameObject, aliveTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = linearMover.Move(transform.position, transform.right, -speed);
        
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
        GameManager gm = GameObject.FindObjectOfType<GameManager>();
        gm.enemyShipKills++;
        Destroy(gameObject);
    }
}
