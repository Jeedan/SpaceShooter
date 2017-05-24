using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServitorFireState : IState
{
    ServitorBoss servitor;

    public int ammo = 5;

    public ServitorFireState(ServitorBoss _servitor)
    {
        servitor = _servitor;
    }

    public void OnEnter()
    {
        //Debug.Log("OnEnter Fire...");
    }

    public void OnExit()
    {
        //  Debug.Log("OnExit Fire...");

    }

    public void OnUpdate()
    {
        if (servitor.PlayerShip != null)
        {
            servitor.RotateTowardsTarget();
            Fire();
        }
        else
        {
            servitor.ChangeState(ServitorBoss.BossPattern.THINKING);
        }
    }


    public void Fire()
    {
        Shoot(servitor.gameObject);
    }

    private void Shoot(GameObject owner)
    {
        if (Time.time > servitor.nextFire && ammo > 0)
        {
            Vector3 direction = (servitor.transform.position - servitor.PlayerShip.transform.position).normalized;
            direction.y = 0.0f;

            if (servitor.HealthPercentage <= 0.5f)
            {
                direction = (servitor.transform.position - servitor.PlayerShip.transform.position).normalized;
            }

            GameObject bulletGO = GameObject.Instantiate(servitor.bulletPrefab, servitor.transform.position + (-direction * (servitor.transform.localScale.x * 1.5f)), Quaternion.identity);
            BulletLinear bullet = bulletGO.GetComponent<BulletLinear>();

            bullet.owner = owner;


            if (servitor.HealthPercentage <= 0.5f)
            {
                bullet.target = servitor.PlayerShip.transform;
                bullet.direction = (servitor.transform.position - servitor.PlayerShip.transform.position).normalized;
            }


            ammo--;
            servitor.nextFire = Time.time + servitor.fireRate;
        }

        if (ammo <= 0)
        {
            servitor.ChangeState(ServitorBoss.BossPattern.THINKING);
            ammo = 5;
        }
    }

}
