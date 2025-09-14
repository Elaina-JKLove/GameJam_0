using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状态机
public class StateMachine
{
    public BaseState CurrentState => currentState;

    BaseState currentState;



    #region Public Methods

    public void Init(BaseState startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    public void ChangeState(BaseState newState)
    {
        ExitState();
        EnterState(newState);
    }

    public void Update() => currentState.Update();

    #endregion

    #region Private Methods

    void EnterState(BaseState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    void ExitState() => currentState.Exit();

    #endregion
}
