using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //all of the state objects will be created in this script
    public StateMachine stm{get;private set;}

    public playerIdleState idleState{get; private set;}

    public playerMovestate moveState{get;private set;}

    public Animator anim{get;private set;}
    public Rigidbody2D rb{get; private set;}

    public playerInput handler{get;private set;}
    [SerializeField] private playerData pd;
    private Vector2 currentVelocity;
    private void Awake(){
        // when the player loads in all needed references will be loaded 
        stm=new StateMachine(); 
        idleState=new playerIdleState(this,stm,pd,"idle");
        moveState=new playerMovestate(this,stm,pd,"move");
        
    }
    
    private void Start() { //when the game starts the player is loaded in with the idle state
        anim=GetComponent<Animator>(); 
        handler=GetComponent<playerInput>();
        stm.Initialize(idleState);
        rb=this.GetComponent<Rigidbody2D>();
    }
    private void Update() { 
        stm.currentState.Logic();
    }
    private void FixedUpdate(){
        stm.currentState.Physics();
        currentVelocity=rb.velocity;
        
    }
    public void movePlayerX(float x)
    {
        float targetSpeed=x*pd.maxSpeed;// calculate the max speed in desired direction
        float speedDiff=targetSpeed-currentVelocity.x;// difference between current speed and max speed
        float accRate=(Mathf.Abs(targetSpeed)>0.01f)?pd.acc:pd.decc; // get the rate needed to reach top speed 
        float moveAmount=Mathf.Pow(Mathf.Abs(speedDiff)*accRate,pd.accPower)*Mathf.Sign(speedDiff);//calculate the move speed
        rb.AddForce(moveAmount*Vector2.right);
        //add friction
         float temp=Mathf.Min(Mathf.Abs(currentVelocity.x),Mathf.Abs(pd.horizontalFriction));
            temp*=Mathf.Sign(currentVelocity.x);
            rb.AddForce(Vector2.right*-temp,ForceMode2D.Impulse);
    }
}
