using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class inputHandler : MonoBehaviour
{
    public Vector2 direction { get; private set; }
    public float lastJumpPressed;
    public bool jumpReleased; 
    public bool dashPressed;
    [SerializeField] player player;
    public Vector2 RawDashDirectionInput{ get; private set; }
    public Vector2Int dashDirectionInput{get;private set;}
    private PlayerInput pInput;
    private Camera cam;
    void Start()
    {
        player=this.GetComponent<player>();
        jumpReleased = false;
        lastJumpPressed = 0;
        pInput=player.GetComponent<PlayerInput>();
        cam=Camera.main;

    }
    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>().normalized;
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
    public void UseDashInput() => dashPressed = false;
    public void Dash(InputAction.CallbackContext context){
        if(context.started){
            dashPressed=true;
        }
    }
    public void onDashDirection(InputAction.CallbackContext context){
        RawDashDirectionInput=context.ReadValue<Vector2>();
        if(pInput.currentControlScheme=="Keyboard&Mouse"){
            RawDashDirectionInput=cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput)-transform.position;
        }
        dashDirectionInput=Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }
}
