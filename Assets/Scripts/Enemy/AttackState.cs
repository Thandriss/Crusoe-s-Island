using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float attackTimer;
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            attackTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            if (attackTimer > enemy.fireRate) {
                Attack();
            }
            float moveTime = Random.Range(3, 7);
            if (moveTimer > moveTime)
            {
                enemy.Agent.SetDestination(enemy.Player.transform.position);
                moveTimer = 0;
            }
            enemy.LastPos = enemy.Player.transform.position;
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                state.ChangeState(new SearchState());
            }
        }
    } 
    public void Attack()
    {
        attackTimer = 0;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
