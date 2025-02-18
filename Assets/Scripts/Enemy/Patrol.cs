using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : BaseState
{
    public int wayInd;
    public float wait;
    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        Cycle();
        if(enemy.CanSeePlayer())
        {
            state.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {
        
    }

    public void Cycle() { 
        if(enemy.Agent.remainingDistance < 0.2f)
        {
            wait += Time.deltaTime;
            if(wait > 3)
            {
                if (wayInd < enemy.way.directionOfPath.Count - 1)
                {
                    wayInd++;
                }
                else
                {
                    wayInd = 0;
                }
                enemy.Agent.SetDestination(enemy.way.directionOfPath[wayInd].position);
                wait = 0;
            }
            
        }
    }
}
