using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerIdleState : playerGroundedState
{
    public playerIdleState(Player player, StateMachine stm, playerData playerData, string animBoolName) : base(player, stm, playerData, animBoolName)
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
        if(input.x!=0){
            stm.changeState(player.moveState);
        }
    }

    public override void Physics()
    {
        base.Physics();
    }
}
