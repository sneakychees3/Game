using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //all of the state objects will be created in this script
    public StateMachine stm{get;private set;}

    private void Awake(){
        stm=new StateMachine(); // when the player loads in a state machine reference will be ready 
    }
    
    private void Start() {
        //initialize state machine    
    }
    private void Update() {
        stm.currentState.Logic();
    }
    private void FixedUpdate(){
        stm.currentState.Physics();
    }
}
