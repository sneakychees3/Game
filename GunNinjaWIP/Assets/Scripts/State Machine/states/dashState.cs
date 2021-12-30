using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashState : playerBase
{
    Vector2 initialVelocity;
    float initialXdirection;
    float dashTimeLeft;
    float lastImageXPos;
    bool dashStopped=false;
    public dashState(player player, stateMachine stm) : base(player, stm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.pd.lastDash=Time.time;
        initialVelocity=rb.velocity;
        initialXdirection=inputs.direction.x;
        rb.velocity=Vector2.zero;
        dashTimeLeft=player.pd.dashTime;
        dash();
    }

    public override void Exit()
    {
        base.Exit();
        dashStopped=false;
    }

    public override void logic()
    {
        base.logic();
        dashTimeLeft-=Time.deltaTime;
        if(Mathf.Sign(inputs.direction.x)!=Mathf.Sign(initialXdirection))
        {
            dashStopped=true;
            Debug.Log("canceled");
        }
        if(dashStopped||dashTimeLeft<=0){
            inputs.dashPressed=false;
            rb.velocity=initialVelocity;
            if(player.isGrounded()&&(Mathf.Abs(inputs.direction.x)<=0.01f)){
                stm.ChangeState(player.idle);
            }
            else if(player.isGrounded()&&(Mathf.Abs(inputs.direction.x)>0.01f)){
                stm.ChangeState(player.move);
            }
            else if(!(player.isGrounded())){
                stm.ChangeState(player.fall);
            }
        }
    }

    public override void physics()
    {
        base.physics();
    }

    private void dash(){
        dashTimeLeft=player.pd.dashTime;
        rb.velocity=new Vector2(player.pd.maxSpeed*player.pd.dashMultiplier*Mathf.Sign(initialVelocity.x),0);
    }
}
