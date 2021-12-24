using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerInput : MonoBehaviour
{
   public Vector2 movement{get;private set;}

   public void onMove(InputAction.CallbackContext context){
       movement=context.ReadValue<Vector2>();
       Debug.Log(movement);
    }
   public void onJump(InputAction.CallbackContext context){
       Debug.Log("jumps");
   }
}
