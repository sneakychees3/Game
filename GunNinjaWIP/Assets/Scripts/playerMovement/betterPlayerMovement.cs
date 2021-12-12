   
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterPlayerMovement : MonoBehaviour{
    //horizontal movement stuff
    Vector2 direction;
    [SerializeField] float maxSpeed=10f;
    [SerializeField] float acc=9f;
    [SerializeField] float decc=9f;
    [SerializeField] float airAcc=1f;
    [SerializeField] float airDecel=1f;
    [SerializeField] float accPower=1.2f;
    [SerializeField] float hFriction=.85f;
    [SerializeField] float jumpForce=12;
    [SerializeField] float jumpCutAmount=0.5f;
    [SerializeField] float gravityScale=1;
    [SerializeField] float fallMultiplier=2f;
    bool isJumping;
    //components
     Rigidbody2D rb;
     betterJump jumpScript;
     Collisions colls;
    //timers
    [SerializeField] float lastGroundedTime=0.1f;
    float lastGrounded=0;
    [SerializeField] float lastJumpPressedTime=0.1f;
    float lastJumpPressed=0;

    void Start() {
        rb=this.GetComponent<Rigidbody2D>();
        jumpScript=this.GetComponent<betterJump>();
        colls=this.GetComponent<Collisions>();
    }
    void Update() {
        direction=new Vector2((Input.GetAxisRaw("Horizontal")),(Input.GetAxisRaw("Vertical")));
        if(rb.velocity.y<0)
        {
            isJumping=false;
        }
        //spacebar timer
        if(lastJumpPressed<0){
            lastJumpPressed=0;
        }else{
            lastJumpPressed-=Time.deltaTime;
        }
        if(Input.GetButtonDown("Jump")){
            lastJumpPressed=lastJumpPressedTime;
        }
        //ground timer
        if(lastGrounded<0){
            lastGrounded=0;
        }else{
            lastGrounded-=Time.deltaTime;
        }
        if(colls.onGround){
            lastGrounded=lastGroundedTime;
            jumpScript.enabled=false;
        }
        
    }
    void FixedUpdate() {
        horizontalMove(direction.x);  
         if(lastGrounded>0&&lastJumpPressed>0&&!isJumping){
        jump(direction,false);
    }else{onJumpUp();} 
         
         //faster fall
         if(rb.velocity.y<0){
             rb.gravityScale=gravityScale*fallMultiplier;
         }
         else{
             rb.gravityScale=gravityScale;
         }
    }
    void horizontalMove(float x){
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
            float temp=Mathf.Min(Mathf.Abs(rb.velocity.x),Mathf.Abs(hFriction));
            temp*=Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right*-temp,ForceMode2D.Impulse);
        }
    }
    void jump(Vector2 dir,bool wall) {
        lastGrounded=0;
        lastJumpPressed=0;
        rb.velocity=new Vector2(rb.velocity.x,0);
        rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
        isJumping=true;
    }
    void onJumpUp(){
        if(rb.velocity.y>0&&isJumping){
            rb.AddForce(Vector2.down*rb.velocity.y*jumpCutAmount,ForceMode2D.Impulse);
        }
    }
}
