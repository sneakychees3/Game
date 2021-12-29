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
        player.pd.lastDash=player.pd.dashCoolDown;
        initialVelocity=rb.velocity;
        initialXdirection=inputs.direction.x;
        dash();
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity=initialVelocity;
    }

    public override void logic()
    {
        base.logic();
        dashTimeLeft-=Time.deltaTime;
        if(Mathf.Abs(initialXdirection-inputs.direction.x)>0.4f)
        {
            dashStopped=true;
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
