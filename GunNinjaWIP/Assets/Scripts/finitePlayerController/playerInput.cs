using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerInput : MonoBehaviour
{
   public Vector2 movement;
   public void onMove(InputAction.CallbackContext context){
       movement=context.ReadValue<Vector2>();
    }
   public void onJump(InputAction.CallbackContext context){
       Debug.Log("Jump");
   }
}
