using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashState : playerBase
{
    public bool isDashing=false;
    float tempCounter=0;
    bool dashStopped=false;
    public dashState(player player, stateMachine stm) : base(player, stm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.pd.lastDash=Time.time;
        rb.gravityScale=0;
        dash();
    }

    public override void Exit()
    {
        base.Exit();
        inputs.dashPressed=false;
        tempCounter=0;
        dashStopped=false;
        player.pd.canDash=false;
    }

    public override void logic()
    {
        base.logic();
        if(Time.time>=player.pd.lastDash+player.pd.dashTime||dashStopped){
            tempCounter++;
            if(tempCounter==1){
            isDashing=false;
            rb.gravityScale=player.pd.gravityScale;
            rb.velocity=Vector2.zero;
            }
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
        if(player.isTouchingWall()==1||player.isTouchingWall()==-1){
            Debug.Log("Wall detected");
            dashStopped=true;
        }
        else if (player.isGrounded()&&rb.velocity.y<0){
            Debug.Log("Ground Dectected");
            dashStopped=true;
        }
    }

    void dash(){
        rb.velocity=inputs.dashDirectionInput*(int)player.pd.dashMultiplier;
        isDashing=true;
    }
}
