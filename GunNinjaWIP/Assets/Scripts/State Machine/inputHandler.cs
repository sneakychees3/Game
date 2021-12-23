using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class inputHandler : MonoBehaviour
{
    public Vector2 direction { get; private set; }
    public float lastJumpPressed { get; private set; }
    public bool jumpReleased { get; private set; }
    [SerializeField] playerBase player;
    void Start()
    {
        jumpReleased = false;
        lastJumpPressed = 0;
    }
    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
    // public void Jump(InputAction.CallbackContext context)
    // {
    //     if (context.started)
    //     {
    //         lastJumpPressed = player.pd.lastJumpPressedTime;
    //     }

    //     if (context.canceled && player.rb.velocity.y > 0 && !jumpReleased)
    //     {
    //         jumpReleased = true;
    //     }
    // }
}