using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class playerBase
{
    #region components
    public Rigidbody2D rb{get;private set;}
    public playerData pd;
    public Renderer r;
    protected Animator anim;
    protected player player;
    protected stateMachine stm;
    protected inputHandler inputs;
    public string animBoolName;
    #endregion
    public float lastGrounded=0f;
    
    
    #region constructor
    public playerBase(player player,stateMachine stm){
        this.player=player;
        this.stm=stm;
        player.facingDirection=1;
        anim=player.GetComponent<Animator>();
        rb=player.GetComponent<Rigidbody2D>();
        inputs=player.GetComponent<inputHandler>();
        r=player.GetComponent<SpriteRenderer>();
    }
    #endregion
    
    public virtual void Enter(){
        anim.SetBool(animBoolName,true);
    }
    public virtual void logic(){
        inputs.lastJumpPressed-=Time.deltaTime;
        if(player.isGrounded()){
            lastGrounded=player.pd.lastGroundedTime;
        }
        else lastGrounded-=Time.deltaTime;
        if(Time.time>=player.pd.lastDash+player.pd.dashCoolDown){
            player.pd.canDash=true;
        }else if(!player.pd.canDash&&inputs.dashPressed){
            inputs.dashPressed=false;
        }
        
    }
    public virtual void physics(){

    }
    public virtual void Exit(){
        anim.SetBool(animBoolName,false);
    }
     
    
}
