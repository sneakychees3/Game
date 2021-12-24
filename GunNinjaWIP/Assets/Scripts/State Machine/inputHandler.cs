using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class inputHandler : MonoBehaviour
{
    public Vector2 direction { get; private set; }
    public float lastJumpPressed;
    public bool jumpReleased; 
    [SerializeField] playerBase player;
    void Start()
    {
        jumpReleased = false;
        lastJumpPressed = 0;
    }
    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        Debug.Log(direction);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            lastJumpPressed = player.pd.lastJumpPressedTime;
            Debug.Log("jump button pressed");
        }

        if (context.canceled && player.rb.velocity.y > 0 && !jumpReleased)
        {
            jumpReleased = true;
            Debug.Log("jump button let go");
        }
    }
    
}
