using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class cameraLookingOffset : MonoBehaviour
{
    [SerializeField] GameObject Player;
    player playerScript;
    CinemachineFramingTransposer cam;
    Vector2 followOffset=Vector2.zero;
    [SerializeField]Vector2 followRightOffset=Vector2.zero;
    [SerializeField]Vector2 followLeftOffset=Vector2.zero;
    void Start(){
        cam=this.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        playerScript=Player.GetComponent<player>();
    }

    void Update(){
        if(playerScript.facingDirection==1&&followOffset!=followRightOffset){
            cam.m_ScreenX=Mathf.Lerp(followOffset.x,followRightOffset.x,1);
            cam.m_ScreenY=Mathf.Lerp(followOffset.y,followRightOffset.y,1);
            followOffset=followRightOffset;
        }
        else if(playerScript.facingDirection==-1&&followOffset!=followLeftOffset){
            cam.m_ScreenX=Mathf.Lerp(followOffset.x,followLeftOffset.x,1);
            cam.m_ScreenY=Mathf.Lerp(followOffset.y,followLeftOffset.y,1);
            followOffset=followLeftOffset;
        }
    }
}
