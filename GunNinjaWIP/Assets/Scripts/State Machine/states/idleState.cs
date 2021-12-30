using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleState : playerBase
{
    public idleState(player player, stateMachine stm) : base(player, stm)
    {

    }

    public override void Enter()
    {
        base.Enter();
        r.material.color=Color.magenta;
        Debug.Log("idleState");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void logic()
    {
        base.logic();
        if((Mathf.Abs(inputs.direction.x)>0.01f)&&(player.isGrounded())){
            stm.ChangeState(player.move);
        }else if(inputs.lastJumpPressed>0&&lastGrounded>0){
            stm.ChangeState(player.jump);
        }else if(rb.velocity.y<0&&lastGrounded<=0){
            stm.ChangeState(player.fall);
        }
        else if(inputs.dashPressed&&player.pd.canDash){
            stm.ChangeState(player.dash);
        }
    }

    public override void physics()
    {
        base.physics();
        rb.velocity=new Vector2(0,0);
    }


   
}
