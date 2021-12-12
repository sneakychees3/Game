using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movement : MonoBehaviour
{
    //components
    Rigidbody2D rb;
    [SerializeField]LayerMask groundLayer;
    BoxCollider2D bc;
    //general
    Vector2 direction;
    //physics stuff
    [SerializeField]float moveSpeed;//avg movespeed
    [SerializeField] float maxSpeed;//fastest speed char can go
    [SerializeField] float lDrag=4f;//linear drag
    [SerializeField] float gravity=1;
    [SerializeField] float fall_multiplier=4;//smaller means higher jump height need to play with this
    //jump stuff
    [SerializeField]bool isGrounded=false;
    [SerializeField] float jumpForce=15f;//force applied to jump
    [SerializeField] float jumpPressedRememberTime=0.2f;
    private float jumpPressedRemember=0;
    [SerializeField] float groundedRememberTime=.2f;
    private float groundedRemember=0;

    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
        bc=this.GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        direction=new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        //need to check if player is touching ground each frame
        isGrounded=Physics2D.BoxCast(this.bc.bounds.center,bc.bounds.size,0f,Vector2.down,.4f,groundLayer);
        jumpPressedRemember-=Time.deltaTime;
        if(jumpPressedRemember<0)
        {
            jumpPressedRemember=0;
        }
        if(Input.GetButtonDown("Jump"))
        {
           jumpPressedRemember=jumpPressedRememberTime;
        }
        groundedRemember-=Time.deltaTime;
        if(groundedRemember<0)
        {
            groundedRemember=0;
        }
        if(isGrounded)
        {
            groundedRemember=groundedRememberTime;
        }
    }
    void FixedUpdate() {
        move(direction.x);
        if((jumpPressedRemember>0)&&groundedRemember>0)
        {
            groundedRemember=0;
            jumpPressedRemember=0;
            Jump();
        }
        modifyPhysics();
    }
     //movement controls below 
     void move(float moveHorizontal) {
        rb.AddForce(Vector2.right* moveHorizontal*moveSpeed);
        //need to mess with animator here to change character direction
        if(Mathf.Abs(rb.velocity.x)>maxSpeed)
        {
            rb.velocity=new Vector2(moveHorizontal*maxSpeed,rb.velocity.y);
        }
    }
    void modifyPhysics(){ //using linear drag to stop character from sliding across the screen when direction keys are let go
        bool changingDirections=(direction.x>0&&rb.velocity.x<0)||(direction.x<0&&rb.velocity.x>0);
        if(isGrounded){
        if(Mathf.Abs(direction.x)<0.4f||changingDirections){
            rb.drag=lDrag;
            }
        else {
        rb.drag=0;
        }
        rb.gravityScale=0;
        }
        else{ 
        rb.gravityScale=gravity;
        rb.drag=lDrag*0.15f;
        if(rb.velocity.y<0)//if we are slowing down upwards use this to slow fall speed down
        {
            rb.gravityScale=gravity*fall_multiplier;
        }
        else if (rb.velocity.y>0&&!Input.GetButtonDown("Jump"))// if we are speed upward but not holding jump button
        {
            rb.gravityScale=gravity*(fall_multiplier/2);
        }
        }
    }
    //overall movement works well, need to play around with values to make it smooth
    //jumping stuff below
    void Jump()
    {
        rb.velocity=new Vector2(rb.velocity.x,0);
        rb.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);//forceMode2D means the force should be applied instantly, no delay 
    }
    //works ok
    //need to add delay so that player can jump if they were on the ground a certain amount of seconds ago, makes it feel more natural
}

        

