using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    float timeMove = Random.Range(3, 5);
    float timeSearch = Random.Range(3, 7);
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.LastPos);
    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            state.ChangeState(new AttackState());
        }
        if (enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance) {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            if (moveTimer > timeMove)
            {
                enemy.Agent.SetDestination(enemy.Player.transform.position);
                moveTimer = 0;
            }
            if (searchTimer > timeSearch) {
                state.ChangeState(new Patrol());
            }
        }
    }
}
