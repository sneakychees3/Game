using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public Rigidbody2D rb{get;private set;}
    [SerializeField] public playerData pd;
    public stateMachine stm{get;private set;}
    
    #region conditions
    Vector2 currentV=Vector2.zero;
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;
    public LayerMask groundLayer;
    #endregion
    #region states
    //all states go here so we can switch between them
    #endregion
    void initializeStates(){
        
        //always initialize new states here 
    }
    void Awake() {
        rb=this.GetComponent<Rigidbody2D>();
        stm=this.GetComponent<stateMachine>();
        initializeStates();
        //stm.Initialize(idle);
    }

    void Update()
    {
        stm.currentState.logic();
        currentV=rb.velocity;
    }
    void FixedUpdate() {
        stm.currentState.physics();
    }
    #region conditions
    public bool isMovingX(){
        return Mathf.Abs(currentV.x)>0.01f;
    }
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
