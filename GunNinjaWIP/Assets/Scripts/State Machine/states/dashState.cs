using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashState : playerBase
{
    public float lastDash;
    public bool isDashing=false;
    public dashState(player player, stateMachine stm) : base(player, stm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        lastDash=Time.time;
        rb.gravityScale=0;
        dash();
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity=Vector2.zero;
        inputs.dashPressed=false;
    }

    public override void logic()
    {
        base.logic();
        if(Time.time>=lastDash+player.pd.dashTime){
            isDashing=false;
            rb.gravityScale=player.pd.gravityScale;
            if(player.isGrounded()&&(Mathf.Abs(inputs.direction.x)>0.01f)){
                stm.ChangeState(player.move);
            }
            else if(inputs.direction.x==0&&player.isGrounded()&&Mathf.Abs(rb.velocity.x)<=0.01f){
                stm.ChangeState(player.idle);
            }
            else if(rb.velocity.y<0&&!player.isGrounded()){
                stm.ChangeState(player.fall);
            }
        }
    }

    public override void physics()
    {
        base.physics();
    }

    void dash(){
        rb.velocity=inputs.dashDirectionInput*(int)player.pd.dashMultiplier;
        isDashing=true;
    }
}
