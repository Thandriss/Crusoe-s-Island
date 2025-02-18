using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState currentState;
    void Start()
    {
        
    }

    void Update()
    {
        if (currentState != null) {
            currentState.Perform();
        }
    }

    public void ChangeState(BaseState state)
    {
        if (currentState != null) {
            currentState.Exit();
        }
        currentState = state;

        if (currentState != null) {
            currentState.state = this;
            currentState.enemy = GetComponent<Enemy>();
            currentState.Enter();
        }
    }

    public void Init()
    {
        ChangeState(new Patrol());
    }
}
