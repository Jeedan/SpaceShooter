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

    // TODO refactor sine wave
    public float frequency = 2.0f; // speed of the sine wave
    public float amplitude = 2.0f; // height of the sine wave
    public float waveOffset = 0.0f; // offset in the wave


    // Use this for initialization
    void Start()
    {
        maxHealth = currentHealth;
        linearMover = new LinearMovement();
     //   Destroy(gameObject, aliveTime);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = linearMover.Move(transform.position, transform.right, -speed);
        WaveMove();
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

    public void WaveMove()
    {
        Vector3 dir = transform.right * -speed * Time.deltaTime;
        Vector3 wave = Vector3.zero;
        float sineWave = Mathf.Sin(Time.time * frequency + waveOffset);
        wave.y =  (sineWave ) * amplitude;

        transform.position += dir + wave * Time.deltaTime;
    }
}
