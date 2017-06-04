using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServitorMoveCenterState : IState
{
    ServitorBoss servitor;
    IEnumerator mover = null;
    
    public ServitorMoveCenterState(ServitorBoss _servitor)
    {
        servitor = _servitor;

    }

    public void OnEnter()
    {
        //Debug.Log("OnEnter Move Center...");
        mover = servitor.MoveServitor(0.50f);

        servitor.StartCoroutine(mover);

        servitor.currPos = 0;
    }

    public void OnExit()
    {
        //Debug.Log("OnExit Move Center...");
        servitor.StopCoroutine(mover);
    }

    public void OnUpdate()
    {
        servitor.RotateTowardsTarget();
    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(2);
        // todo change state to random up down or center depending on where you started
        servitor.ChangeState(ServitorBoss.BossPattern.FIRE);
    }
}