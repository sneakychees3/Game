using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterPlayerMovement : MonoBehaviour
{
    #region Movement
    [SerializeField] float maxSpeed=10f;
    [SerializeField] float acc=9f;
    [SerializeField] float decc=9f;
    [SerializeField] float airAcc=1f;
    [SerializeField] float airDecel=1f;
    [SerializeField] float accPower=1.2f;
    #endregion
    #region Jump
    [SerializeField] float jumpForce=12f;
    [SerializeField] float jumpCutAmount=.1f;
    [SerializeField] float fallMultiplier=1f;
    [SerializeField] float lastGroundedTime=0.05f;
    float lastGrounded=0;
    [SerializeField] float lastJumpPressedTime=0.1f;
    float lastJumpPressed=0;
    [SerializeField] float gravityScale=1;
    [SerializeField]bool isGrounded;
    bool isJumping=false;
    [SerializeField] float jumpTimeCounter=.1f;
    float jumpTime;
    #endregion
    // components 
    Rigidbody2D rb;
    BoxCollider2D bc;
    //basic movement
    Vector2 direction;
    [SerializeField]float frictionAmount=0;
    //other
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform feetPos;
    [SerializeField] float checkRadius;
    void Start(){
        rb=this.GetComponent<Rigidbody2D>();
        bc=this.GetComponent<BoxCollider2D>();
    }
    void Update(){
        direction=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        
        isGrounded=Physics2D.OverlapCircle(feetPos.position,checkRadius,groundLayer);
        if(lastJumpPressed<0){
            lastJumpPressed=0;
        }
        else{
            lastJumpPressed-=Time.deltaTime;
        }
        if(Input.GetButtonDown("Jump")){
            lastJumpPressed=lastJumpPressedTime;
        }
        if(lastGrounded<0){
            lastGrounded=0;
        }
        else{
            lastGrounded-=Time.deltaTime;
        }
        if(isGrounded){
            isJumping=false;
            lastGrounded=lastGroundedTime;
        }
        if(lastJumpPressed>0&&lastGrounded>0){
            jump();
            onJumpUp();
        }
    }
    private void FixedUpdate() {
        playerMove(direction.x);
        if(rb.velocity.y<0){
            rb.gravityScale=gravityScale*fallMultiplier;
        }else{
            rb.gravityScale=gravityScale;
        }
    }
    void playerMove(float xDirection) {
        if(Mathf.Abs(rb.velocity.y)<=0){
        float targetSpeed=xDirection*maxSpeed;// calculate the max speed in desired direction
        float speedDiff=targetSpeed-rb.velocity.x;// difference between current speed and max speed
        float accRate=(Mathf.Abs(targetSpeed)>0.01f)?acc:decc; // get the rate needed to reach top speed 
        float moveAmount=Mathf.Pow(Mathf.Abs(speedDiff)*accRate,accPower)*Mathf.Sign(speedDiff);//calculate the move speed
        rb.AddForce(moveAmount*Vector2.right);
        }
        else
        {
        float targetSpeed=xDirection*maxSpeed;// calculate the max speed in desired direction
        float speedDiff=targetSpeed-rb.velocity.x;// difference between current speed and max speed
        float accRate=(Mathf.Abs(targetSpeed)>0.01f)?airAcc:airDecel; // get the rate needed to reach top speed 
        float moveAmount=Mathf.Pow(Mathf.Abs(speedDiff)*accRate,accPower)*Mathf.Sign(speedDiff);//calculate the move speed
        rb.AddForce(moveAmount*Vector2.right);
        }
        
        //adding friction
        if(lastGrounded>0&&Mathf.Abs(xDirection)<0.1) {
            float temp=Mathf.Min(Mathf.Abs(rb.velocity.x),Mathf.Abs(frictionAmount));
            temp*=Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right*-temp,ForceMode2D.Impulse);
        }
    }
    void jump() {
        lastGrounded=0f;
        lastJumpPressed=0f;
        rb.velocity=new Vector2(rb.velocity.x,0);
        rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
        jumpTime=jumpTimeCounter;
        isJumping=true;
    }
    void onJumpUp(){
        if(rb.velocity.y>0&&isJumping){
            rb.AddForce(Vector2.down*rb.velocity.y*jumpCutAmount,ForceMode2D.Impulse);
        }
    }
}
