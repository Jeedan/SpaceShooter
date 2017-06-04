using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShipWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float damage = 1.0f;
    public float fireRate = 0.3f;
    private float nextFire;

    private GameObject owner;

    // Use this for initialization
    void Start()
    {

    }



    public void Shoot(GameObject owner)
    {
        if(Time.time > nextFire)
        {
            GameObject bulletGO = Instantiate(bulletPrefab, transform.position + (transform.right * (transform.localScale.x * 0.5f)), Quaternion.identity);
            bulletGO.GetComponent<BulletLinear>().owner = owner;
            nextFire = Time.time + fireRate;
        }
    }
}
