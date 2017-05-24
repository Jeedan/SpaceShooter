using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServitorMoveDownState : IState
{
    ServitorBoss servitor;
    IEnumerator mover = null;

    public ServitorMoveDownState(ServitorBoss _servitor)
    {
        servitor = _servitor;

    }

    public void OnEnter()
    {
        //Debug.Log("OnEnter Move down...");

        mover = servitor.MoveServitor(0.20f);

        servitor.StartCoroutine(mover);

        servitor.currPos = 2;
    }

    public void OnExit()
    {
       // Debug.Log("OnExit Move down...");
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