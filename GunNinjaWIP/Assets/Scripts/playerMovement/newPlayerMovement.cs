using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class newPlayerMovement : MonoBehaviour
{
    //components
    [SerializeField]Rigidbody2D rb;
    Collisions colls;
    [Header("Movement Stuff")]
    Vector2 direction;
    [SerializeField] float maxSpeed=10f;
    [SerializeField] float acc=9f;
    [SerializeField] float decc=9f;
    [SerializeField] float airAcc=1f;
    [SerializeField] float airDecel=1f;
    [SerializeField] float accPower=1.2f;
    [SerializeField] float horizontalFriction=.85f;
    
    [Space(10)]
    [Header("Jump Stuff")]
    [SerializeField] float jumpForce=12;
    [SerializeField] float jumpCutAmount=0.5f;
    [SerializeField] float gravityScale=1;
    [SerializeField] float fallMultiplier=2f;
    [Space(10)]
    [Header("Timers")]
    [SerializeField] float lastGroundedTime=0.1f;
    float lastGrounded=0;
    [SerializeField] float lastJumpPressedTime=0.1f;
    float lastJumpPressed=0;
    //other bools
    bool isJumping=false;
    bool jumpReleased=false;
    bool jumpPressed=false;
    void Start(){
        rb=this.GetComponent<Rigidbody2D>();
        colls=this.GetComponent<Collisions>();
    }

    void Update() {
        //spacebar timer
        if(lastJumpPressed<0){
            lastJumpPressed=0;
        }else{
            lastJumpPressed-=Time.deltaTime;
        }
        if(jumpPressed){
            lastJumpPressed=lastJumpPressedTime;
            jumpPressed=false;
        }
        //ground timer
        if(lastGrounded<0){
            lastGrounded=0;
        }else{
            lastGrounded-=Time.deltaTime;
        }
        if(colls.onGround){
            lastGrounded=lastGroundedTime;
        }
    }
    void FixedUpdate(){
        applyMovement(direction.x);
        if(lastGrounded>0&&lastJumpPressed>0&&!isJumping){
            applyJump();
        }
        if(rb.velocity.y>0&&isJumping&&jumpReleased){
            rb.AddForce(Vector2.down*rb.velocity.y*jumpCutAmount,ForceMode2D.Impulse);
        }
        if(rb.velocity.y<0){
            rb.gravityScale=gravityScale*fallMultiplier;
        }
        else{
            rb.gravityScale=gravityScale;
        }
        resetBools();
    }
    public void Move(InputAction.CallbackContext context){
        direction=context.ReadValue<Vector2>();
    }
     public void Jump(InputAction.CallbackContext context){
        jumpPressed=true;
        if(context.canceled&&rb.velocity.y>0&&!jumpReleased){
            jumpReleased=true;
        }
    }
    public void applyMovement(float x){
        if(Mathf.Abs(rb.velocity.y)<=0){
        float targetSpeed=x*maxSpeed;// calculate the max speed in desired direction
        float speedDiff=targetSpeed-rb.velocity.x;// difference between current speed and max speed
        float accRate=(Mathf.Abs(targetSpeed)>0.01f)?acc:decc; // get the rate needed to reach top speed 
        float moveAmount=Mathf.Pow(Mathf.Abs(speedDiff)*accRate,accPower)*Mathf.Sign(speedDiff);//calculate the move speed
        rb.AddForce(moveAmount*Vector2.right);
        }
        else
        {
        float targetSpeed=x*maxSpeed;// calculate the max speed in desired direction
        float speedDiff=targetSpeed-rb.velocity.x;// difference between current speed and max speed
        float accRate=(Mathf.Abs(targetSpeed)>0.01f)?airAcc:airDecel; // get the rate needed to reach top speed 
        float moveAmount=Mathf.Pow(Mathf.Abs(speedDiff)*accRate,accPower)*Mathf.Sign(speedDiff);//calculate the move speed
        rb.AddForce(moveAmount*Vector2.right);
        }
        //adding friction
        if(lastGrounded>0&&Mathf.Abs(x)<0.1) {
            float temp=Mathf.Min(Mathf.Abs(rb.velocity.x),Mathf.Abs(horizontalFriction));
            temp*=Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right*-temp,ForceMode2D.Impulse);
        }
    }
   public void applyJump(){
       lastGrounded=0;
        lastJumpPressed=0;
        rb.velocity=new Vector2(rb.velocity.x,0);
        rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
        isJumping=true;
   }
    public void resetBools(){
        if(colls.onGround&&rb.velocity.y==0){
            isJumping=false;
            jumpReleased=false;
            jumpPressed=false;
            rb.gravityScale=gravityScale;
        }
    }
}
