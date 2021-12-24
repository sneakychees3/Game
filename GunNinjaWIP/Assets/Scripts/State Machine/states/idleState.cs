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
        Debug.Log("idleState");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void logic()
    {
        base.logic();
        if((Mathf.Abs(direction.x)>0.01f)&&player.isGrounded()){
            stm.ChangeState(player.move);
        }
        else if(lastJumpPressed>0&&lastGrounded>0){
            stm.ChangeState(player.jump);
        }
    }

    public override void physics()
    {
        base.physics();
    }


   
}
