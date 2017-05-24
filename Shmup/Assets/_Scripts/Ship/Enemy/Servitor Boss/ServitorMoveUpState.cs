using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServitorMoveUpState : IState
{
    ServitorBoss servitor;
    IEnumerator mover = null;

    public ServitorMoveUpState(ServitorBoss _servitor)
    {
        servitor = _servitor;

    }

    public void OnEnter()
    {
        //Debug.Log("OnEnter Move up...");
        
        mover = servitor.MoveServitor(0.80f);

        servitor.StartCoroutine(mover);

        servitor.currPos = 1;
    }

    public void OnExit()
    {
        //Debug.Log("OnExit Move up...");
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