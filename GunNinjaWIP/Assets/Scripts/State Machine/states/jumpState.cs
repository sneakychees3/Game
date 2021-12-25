using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpState : playerBase
{
    public bool isJumping=false;
    public jumpState(player player, stateMachine stm) : base(player, stm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("jumpState");
        applyJump();
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
    }

    public void applyJump(){
        lastGrounded=0;
        inputs.lastJumpPressed=0;
        rb.velocity=new Vector2(rb.velocity.x,0);
        rb.AddForce(Vector2.up*player.pd.jumpForce,ForceMode2D.Impulse);
        isJumping=true;
   }
}
