using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class playerBase
{
    #region components
    protected Rigidbody2D rb;
    [SerializeField] playerData pd;
    protected Collisions colls;
    protected Animator anim;
    protected player player;
    protected stateMachine stm;
    #endregion
    
    #region input getters
    public Vector2 direction { get; private set; }
    public float lastJumpPressed { get; private set; }
    public bool jumpReleased { get; private set; }
    #endregion 
    #region constructor
    public playerBase(player player,stateMachine stm){
        this.player=player;
        this.stm=stm;
        anim=player.GetComponent<Animator>();
        rb=player.GetComponent<Rigidbody2D>();
        colls=player.GetComponent<Collisions>();
    }
    #endregion
    private void Start() {
        lastJumpPressed=0;
        jumpReleased=false;
    }
    

    #region inputs
    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            lastJumpPressed = player.pd.lastJumpPressedTime;
        }

        if (context.canceled && player.rb.velocity.y > 0 && !jumpReleased)
        {
            jumpReleased = true;
        }
    }
    #endregion
    
    public virtual void Enter(){

    }
    public virtual void logic(){

    }
    public virtual void physics(){

    }
    public virtual void Exit(){
        
    }
}
