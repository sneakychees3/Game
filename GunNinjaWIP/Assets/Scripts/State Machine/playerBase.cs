using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class playerBase
{
    #region components
    public Rigidbody2D rb{get;private set;}
    public playerData pd;
    //protected Collisions colls;
    protected Animator anim;
    protected player player;
    protected stateMachine stm;
    protected inputHandler inputs;
    #endregion
    public float lastGrounded=0f;
    #region constructor
    public playerBase(player player,stateMachine stm){
        this.player=player;
        this.stm=stm;
        anim=player.GetComponent<Animator>();
        rb=player.GetComponent<Rigidbody2D>();
        inputs=player.GetComponent<inputHandler>();
        //colls=player.GetComponent<Collisions>();
    }
    #endregion
    
    public virtual void Enter(){

    }
    public virtual void logic(){
        inputs.lastJumpPressed-=Time.deltaTime;
        if(player.isGrounded()){
            lastGrounded=player.pd.lastGroundedTime;
        }
        else lastGrounded-=Time.deltaTime;
    }
    public virtual void physics(){

    }
    public virtual void Exit(){
        
    }
}
