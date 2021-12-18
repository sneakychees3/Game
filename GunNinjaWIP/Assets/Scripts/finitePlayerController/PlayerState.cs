using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    //This class is the base class for all of our possible player states
    //any state we make will inheret from this class
public class PlayerState 
{
    protected Player player;//reference to player script 
    protected StateMachine stm;
    protected playerData playerData;
    //animator reference here(dont have yet :( )
    private string aniBoolName;//not needed yet, to switch states in the animator
    protected float startTime;

    public PlayerState(Player player,StateMachine stm,playerData playerData,string animBoolName){
        this.player=player;
        this.stm=stm;
        this.playerData=playerData;
        this.aniBoolName=animBoolName;
    }

    public virtual void Enter(){ //call when enter a new state 
        doCheck();
        startTime=Time.time;
    }
    public virtual void Exit(){//call when leave previous state 

    }
    public virtual void Logic(){//will work as update

    }
    public virtual void Physics(){//will work as fixed update
        doCheck();
    }
    public virtual void doCheck(){//going to do all checks here, looking for enemy/look for ground etc call this in physics and enter

    }

}
