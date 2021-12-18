using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="playerData",menuName="Data/Player Data/Base Data")]
// all the data that can be referenced will be put here, movement force, speed etc.
public class playerData : ScriptableObject
{
    Collisions colls;
    [Header("Movement Stuff")]
    Vector2 direction;
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
    float lastGrounded=0;
    public float lastJumpPressedTime=0.1f;
    float lastJumpPressed=0;
    
}
