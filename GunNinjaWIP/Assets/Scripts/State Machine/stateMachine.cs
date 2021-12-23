using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateMachine : MonoBehaviour
{
    public playerBase currentState{get;private set;}

    public void Initialize(playerBase startingState)
    {
        currentState=startingState;
        startingState.Enter();
    }
    public void ChangeState(playerBase newState) {
        currentState.Exit();
        currentState=newState;
        newState.Enter();
    }
}
