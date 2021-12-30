using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallState : playerBase
{
    public fallState(player player, stateMachine stm) : base(player, stm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        r.material.color=Color.black;
        rb.gravityScale=player.pd.gravityScale*player.pd.fallMultiplier;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale=player.pd.gravityScale;
         resetJumpBools();
    }

    public override void logic()
    {
        base.logic();
        if(rb.velocity.y==0&&player.isGrounded()){
            stm.ChangeState(player.idle);
        } else if(inputs.dashPressed&&player.pd.canDash){
            stm.ChangeState(player.dash);
        }else if((Mathf.Abs(inputs.direction.x)>0.01f)&&(player.isGrounded())){
            stm.ChangeState(player.move);
    }
    }

    public override void physics()
    {
        base.physics();
        airMovement();
    }
    public void airMovement(){
        float targetSpeed=inputs.direction.x*player.pd.maxSpeed;// calculate the max speed in desired direction
        float speedDiff=targetSpeed-rb.velocity.x;// difference between current speed and max speed
        float accRate=(Mathf.Abs(targetSpeed)>0.01f)?player.pd.airAcc:player.pd.airDecel; // get the rate needed to reach top speed 
        float moveAmount=Mathf.Pow(Mathf.Abs(speedDiff)*accRate,player.pd.accPower)*Mathf.Sign(speedDiff);//calculate the move speed
        rb.AddForce(moveAmount*Vector2.right);
   }
   void resetJumpBools(){
       player.isJumping=false;
       inputs.jumpReleased=false;
   }
}
