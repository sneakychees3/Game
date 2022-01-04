using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    #region interact stuff
    [Space(10)]
	[Header("Toggle for the gui on off")]
	public bool NPCGuiOn;

	[Space(10)]
	[Header("The text to Display on Trigger")]
	public string Text = "test";
	public Rect BoxSize = new Rect( 0, 0, 200, 100);

	[Space(10)]
	public GUISkin customSkin;
    #endregion
    
    public Rigidbody2D rb{get;private set;}
    [SerializeField] public playerData pd;
    public stateMachine stm{get;private set;}
    public LayerMask groundLayer;
    
    #region conditions
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;
    public bool isJumping=false;
    #endregion
    
    #region states
    public idleState idle;
    public moveState move;
    public jumpState jump;
    public fallState fall;
    public dashState dash;
    #endregion
    void initializeStates(){
        idle=new idleState(this,stm);
        move=new moveState(this,stm);
        jump=new jumpState(this,stm);
        fall=new fallState(this,stm);
        dash=new dashState(this,stm);
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

        if(!onRightWall&&!onLeftWall){
            return 0;
        }else
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
    void OnTriggerEnter2D(Collider2D other) {
       if(other.gameObject.tag=="NPC"){
            NPCGuiOn=true;
        }
    }  
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="NPC"){
            NPCGuiOn=false;
        }
    }
    void OnGUI()
	{

		if (customSkin != null)
		{
			GUI.skin = customSkin;
		}

		if (NPCGuiOn == true)
		{
			// Make a group on the center of the screen
			GUI.BeginGroup (new Rect ((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));
			// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

			GUI.Label(BoxSize, Text);

			// End the group we started above. This is very important to remember!
			GUI.EndGroup ();

		}


	}
}
