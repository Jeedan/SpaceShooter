using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServitorThinkingState : IState
{
    ServitorBoss servitor;
    IEnumerator thinking = null;

    public ServitorThinkingState(ServitorBoss _servitor)
    {
        servitor = _servitor;
    }

    public void OnEnter()
    {

       // Debug.Log("OnEnter Thinking...");
        thinking = Think();
        servitor.StartCoroutine(thinking);
    }

    public void OnExit()
    {
        //Debug.Log("OnExit Thinking...");
        servitor.StopCoroutine(thinking);
        // thinking = null;
    }

    public void OnUpdate()
    {
        servitor.RotateTowardsTarget();
    }

    IEnumerator Think()
    {

       // Debug.Log("Think...");
        yield return new WaitForSeconds(2);
        // todo change state to random up down or center depending on where you started

        if (servitor.currPos == 0)
        {
            servitor.ChangeState(ServitorBoss.BossPattern.MOVE_UP);
        }
        else if (servitor.currPos == 1)
        {
            servitor.ChangeState(ServitorBoss.BossPattern.MOVE_DOWN);
        }
        else if (servitor.currPos == 2)
        {
            servitor.ChangeState(ServitorBoss.BossPattern.MOVE_CENTER);
        }

    }
}
