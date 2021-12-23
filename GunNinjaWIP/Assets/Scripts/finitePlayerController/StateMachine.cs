using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    //has a reference to the current state we want to be in 
   public PlayerState currentState{get;private set;} //the current player state, made getter and private setter

   public void Initialize(PlayerState startingState){ // call this when player just loaded
       currentState=startingState;
       currentState.Enter();
   }
   public void changeState(PlayerState newState){ // to change the player state
       currentState.Exit();
       currentState=newState;
       newState.Enter();
   }
}
