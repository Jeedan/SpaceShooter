using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInRandomDirection : MonoBehaviour
{
    Vector3 randomPoint;
    Vector3 direction;
    private float explosionForce = 2.0f;
    public float minExplosionForce = 0.0f;
    public float maxExplosionForce = 4.0f;



    public float gibLife = 1.0f;

    SpriteRenderer gibSprite;

    bool gravity = false;
    void Start()
    {
        gibSprite = GetComponent<SpriteRenderer>();
        direction = Random.insideUnitCircle;
        // Debug.Log("explosion direction: " + direction);

        explosionForce = Random.Range(minExplosionForce, maxExplosionForce);

        Destroy(gameObject, gibLife);
        StartCoroutine(FadeBeforeDestroy());
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += (direction).normalized * explosionForce * Time.deltaTime;
        if (gravity)
        {
            direction.y -= 0.2f * Time.deltaTime;
        }
    }

    IEnumerator FadeBeforeDestroy()
    {
        float t = 0;
        float speedFactor = 0.1f;
        while (t < gibLife)
        {
            t += Time.deltaTime;
           // gibSprite.color = new Color(gibSprite.color.r, gibSprite.color.g, gibSprite.color.b, newAlpha);
            gibSprite.color = Color.Lerp(gibSprite.color, Color.clear, t * speedFactor / gibLife);

            yield return null;
        }


    }
}
