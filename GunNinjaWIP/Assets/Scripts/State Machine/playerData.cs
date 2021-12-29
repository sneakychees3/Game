using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="playerData",menuName="Data/Player Data/Base Data")]
public class playerData : ScriptableObject
{
    
    [Header("Movement Stuff")]
    public float maxSpeed=10f;
    public float acc=9f;
    public float decc=9f;
    public float airAcc=1f;
    public float airDecel=1f;
    public float accPower=1.2f;
    public float horizontalFriction=.85f;
    
    [Space(10)]
    [Header("Jump Stuff")]
    public float jumpForce=12;
    public float jumpCutAmount=0.5f;
    public float gravityScale=1;
    public float fallMultiplier=2f;
    [Space(10)]
    [Header("Timers")]
    public float lastGroundedTime=0.1f;
    public float lastJumpPressedTime=0.1f;
    [Header("Dash stuff")]
    public float dashMultiplier=5;
    public float dashCoolDown=1f;
    public float dashTime=1f;
    public float distanceBetweenImages=0.5f;
    public float lastDash=-100;
    
}
