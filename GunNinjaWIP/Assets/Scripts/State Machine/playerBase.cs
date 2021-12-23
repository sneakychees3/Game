using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class playerBase
{
    protected Rigidbody2D rb;
    [SerializeField] playerData pd;
    protected Collisions colls;
    protected Animator anim;
    protected player player;
    protected stateMachine stm;
    
    public playerBase(player player,stateMachine stm){
        this.player=player;
        this.stm=stm;
        anim=player.GetComponent<Animator>();
        rb=player.GetComponent<Rigidbody2D>();
        colls=player.GetComponent<Collisions>();
    }

    public virtual void Enter(){

    }
    public virtual void logic(){

    }
    public virtual void physics(){

    }
    public virtual void Exit(){
        
    }
}
