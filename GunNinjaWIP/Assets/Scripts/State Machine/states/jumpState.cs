using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpState : playerBase
{
    
    public jumpState(player player, stateMachine stm) : base(player, stm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("jumpState");
        r.material.color=Color.green;
        applyJump();
    }

    public override void Exit()
    {
        base.Exit();
        rb.AddForce(Vector2.down*rb.velocity.y*player.pd.jumpCutAmount,ForceMode2D.Impulse);
    }

    public override void logic()
    {
        base.logic();
        if(inputs.jumpReleased||rb.velocity.y<0){
            stm.ChangeState(player.fall);
        }else if(inputs.dashPressed&&player.pd.canDash){
            stm.ChangeState(player.dash);
        }
    }

    public override void physics()
    {
        base.physics();
        airMovement();
    }

    public void applyJump(){
        lastGrounded=0;
        inputs.lastJumpPressed=0;
        rb.velocity=new Vector2(rb.velocity.x,0);
        rb.AddForce(Vector2.up*player.pd.jumpForce,ForceMode2D.Impulse);
        player.isJumping=true;
   }
   public void airMovement(){
        float targetSpeed=inputs.direction.x*player.pd.maxSpeed;// calculate the max speed in desired direction
        float speedDiff=targetSpeed-rb.velocity.x;// difference between current speed and max speed
        float accRate=(Mathf.Abs(targetSpeed)>0.01f)?player.pd.airAcc:player.pd.airDecel; // get the rate needed to reach top speed 
        float moveAmount=Mathf.Pow(Mathf.Abs(speedDiff)*accRate,player.pd.accPower)*Mathf.Sign(speedDiff);//calculate the move speed
        rb.AddForce(moveAmount*Vector2.right);
   }
}
