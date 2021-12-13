using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;//to get position of player
    [SerializeField]public float smoothSpeed=0.125f;// higher this value is the faster the camera will snap to target
    [SerializeField] Vector3 offset;

     void LateUpdate() { //like update function but runs after 
        Vector3 wantedPos=target.position+offset;
        Vector3 smoothedPosition=Vector3.Lerp(transform.position,wantedPos,smoothSpeed);
        this.transform.position=smoothedPosition;
    }
    //basic camera follow, feels weird need something with vector threhsolds i.e mario
    //also need to be able to adjust for mouse movement to use gun
}
