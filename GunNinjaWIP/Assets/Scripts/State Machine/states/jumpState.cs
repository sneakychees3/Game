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
        Debug.Log("jump");
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

}
