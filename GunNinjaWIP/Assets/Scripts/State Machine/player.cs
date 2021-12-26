using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public Rigidbody2D rb{get;private set;}
    [SerializeField] public playerData pd;
    public stateMachine stm{get;private set;}

    #region conditions
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;
    public LayerMask groundLayer;
    public bool isJumping=false;
    #endregion
    #region states
    public idleState idle;
    public moveState move;
    public jumpState jump;
    public fallState fall;
    #endregion
    void initializeStates(){
        idle=new idleState(this,stm);
        move=new moveState(this,stm);
        jump=new jumpState(this,stm);
        fall=new fallState(this,stm);
    }
    void Awake() {
        rb=this.GetComponent<Rigidbody2D>();
        stm=this.GetComponent<stateMachine>();
        this.initializeStates();
        stm.Initialize(this.idle);
    }

    void Update()
    {
        stm.currentState.logic();
    }
    void FixedUpdate() {
        stm.currentState.physics();
    }
    #region conditions
    public bool isGrounded(){
        return Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
    }
    public int isTouchingWall(){
        bool onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        bool onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        return onRightWall ? -1 : 1;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
    #endregion
}
