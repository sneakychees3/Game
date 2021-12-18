using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGroundedState : PlayerState
{
    protected Vector2 input;  
    public playerGroundedState(Player player, StateMachine stm, playerData playerData, string animBoolName) : base(player, stm, playerData,animBoolName)
    {
    
    }

    public override void doCheck()
    {
        base.doCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Logic()
    {
        base.Logic();
        input=player.handler.movement;
    }

    public override void Physics()
    {
        base.Physics();
    }

}
