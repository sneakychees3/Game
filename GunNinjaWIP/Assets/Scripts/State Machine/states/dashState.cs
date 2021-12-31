using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashState : playerBase
{
    Vector2 initialVelocity;
    Vector2 initialDirection;
    public float lastDash;
    public dashState(player player, stateMachine stm) : base(player, stm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        initialVelocity =rb.velocity;
        initialDirection=inputs.direction;
        lastDash=Time.time;
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

    void dash(){
        rb.velocity=new Vector2();
    }
}
