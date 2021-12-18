using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class newPlayerMovement : MonoBehaviour
{ 
    #region setup
    private Vector2 direction;
    private bool isJumping=false;
    private bool jumpReleased=false;
    private bool jumpPressed=false;

    private float lastJumpPressed=0;
    private float lastGrounded=0;
    private Rigidbody2D rb;
    [SerializeField]private playerData pd;
    Collisions colls;
    void Start(){
        rb=this.GetComponent<Rigidbody2D>();
        colls=this.GetComponent<Collisions>();
    }
    #endregion
    #region update
    void Update() {
        //spacebar timer
        if(lastJumpPressed<0){
            lastJumpPressed=0;
        }else{
            lastJumpPressed-=Time.deltaTime;
        }
        if(jumpPressed){
            lastJumpPressed=pd.lastJumpPressedTime;
            jumpPressed=false;
        }
        //ground timer
        if(lastGrounded<0){
            lastGrounded=0;
        }else{
            lastGrounded-=Time.deltaTime;
        }
        if(colls.onGround){
            lastGrounded=pd.lastGroundedTime;
        }
        
    }
    #endregion  
    #region  fixedUpdate
    void FixedUpdate(){
        applyMovement(direction.x);
        if(lastGrounded>0&&lastJumpPressed>0&&!isJumping){
            applyJump();
        }
        if(rb.velocity.y>0&&isJumping&&jumpReleased){
            rb.AddForce(Vector2.down*rb.velocity.y*pd.jumpCutAmount,ForceMode2D.Impulse);
        }
        if(rb.velocity.y<0){
            rb.gravityScale=pd.gravityScale*pd.fallMultiplier;
        }
        else{
            rb.gravityScale=pd.gravityScale;
        }
        resetBools();
    }
    #endregion 
    #region horizontal inputs
    public void Move(InputAction.CallbackContext context){
        direction=context.ReadValue<Vector2>();
    }
    #endregion
    #region Jump inputs
     public void Jump(InputAction.CallbackContext context){
        if(context.started){
        jumpPressed=true;
        }
        
        if(context.canceled&&rb.velocity.y>0&&!jumpReleased){
            jumpReleased=true;
        }
    }
    #endregion
    #region horizontalMovement Calcs
    public void applyMovement(float x){
        if(Mathf.Abs(rb.velocity.y)<=0){
        float targetSpeed=x*pd.maxSpeed;// calculate the max speed in desired direction
        float speedDiff=targetSpeed-rb.velocity.x;// difference between current speed and max speed
        float accRate=(Mathf.Abs(targetSpeed)>0.01f)?pd.acc:pd.decc; // get the rate needed to reach top speed 
        float moveAmount=Mathf.Pow(Mathf.Abs(speedDiff)*accRate,pd.accPower)*Mathf.Sign(speedDiff);//calculate the move speed
        rb.AddForce(moveAmount*Vector2.right);
        }
        else
        {
        float targetSpeed=x*pd.maxSpeed;// calculate the max speed in desired direction
        float speedDiff=targetSpeed-rb.velocity.x;// difference between current speed and max speed
        float accRate=(Mathf.Abs(targetSpeed)>0.01f)?pd.airAcc:pd.airDecel; // get the rate needed to reach top speed 
        float moveAmount=Mathf.Pow(Mathf.Abs(speedDiff)*accRate,pd.accPower)*Mathf.Sign(speedDiff);//calculate the move speed
        rb.AddForce(moveAmount*Vector2.right);
        }
        //adding friction
        if(lastGrounded>0&&Mathf.Abs(x)<0.1) {
            float temp=Mathf.Min(Mathf.Abs(rb.velocity.x),Mathf.Abs(pd.horizontalFriction));
            temp*=Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right*-temp,ForceMode2D.Impulse);
        }
    }
    #endregion
    #region Jump Calcs
   public void applyJump(){
       lastGrounded=0;
        lastJumpPressed=0;
        rb.velocity=new Vector2(rb.velocity.x,0);
        rb.AddForce(Vector2.up*pd.jumpForce,ForceMode2D.Impulse);
        isJumping=true;
   }
   #endregion
    #region other stuff
    public void resetBools(){
        if(colls.onGround&&rb.velocity.y==0){
            isJumping=false;
            jumpReleased=false;
            jumpPressed=false;
            rb.gravityScale=pd.gravityScale;
        }
    }
    #endregion
}
