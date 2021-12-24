using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveState : playerBase
{
    public moveState(player player, stateMachine stm) : base(player, stm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("moveState");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void logic()
    {
        base.logic();
       
    }

    public override void physics()
    {
        base.physics();
        applyMovement();
    }

    private void applyMovement(){
        //movement
        float targetSpeed=inputs.direction.x*player.pd.maxSpeed;// calculate the max speed in desired direction
        float speedDiff=targetSpeed-rb.velocity.x;// difference between current speed and max speed
        float accRate=(Mathf.Abs(targetSpeed)>0.01f)?player.pd.acc:player.pd.decc; // get the rate needed to reach top speed 
        float moveAmount=Mathf.Pow(Mathf.Abs(speedDiff)*accRate,player.pd.accPower)*Mathf.Sign(speedDiff);//calculate the move speed
        rb.AddForce(moveAmount*Vector2.right);
        //friction
        if(lastGrounded>0&&Mathf.Abs(inputs.direction.x)<0.1) {
            float temp=Mathf.Min(Mathf.Abs(rb.velocity.x),Mathf.Abs(player.pd.horizontalFriction));
            temp*=Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right*-temp,ForceMode2D.Impulse);
        }
    }
    
}
