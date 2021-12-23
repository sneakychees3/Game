using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizontalMovement : MonoBehaviour
{
    [SerializeField] mainPlayerScrpt player;
    private void FixedUpdate() {
        if(Mathf.Abs(player.rb.velocity.y)<=0){
        float targetSpeed=player.handler.movement.x*player.pd.maxSpeed;// calculate the max speed in desired direction
        float speedDiff=targetSpeed-player.rb.velocity.x;// difference between current speed and max speed
        float accRate=(Mathf.Abs(targetSpeed)>0.01f)?player.pd.acc:player.pd.decc; // get the rate needed to reach top speed 
        float moveAmount=Mathf.Pow(Mathf.Abs(speedDiff)*accRate,player.pd.accPower)*Mathf.Sign(speedDiff);//calculate the move speed
        player.rb.AddForce(moveAmount*Vector2.right);
        }
        else
        {
        float targetSpeed=player.handler.movement.x*player.pd.maxSpeed;// calculate the max speed in desired direction
        float speedDiff=targetSpeed-player.rb.velocity.x;// difference between current speed and max speed
        float accRate=(Mathf.Abs(targetSpeed)>0.01f)?player.pd.airAcc:player.pd.airDecel; // get the rate needed to reach top speed 
        float moveAmount=Mathf.Pow(Mathf.Abs(speedDiff)*accRate,player.pd.accPower)*Mathf.Sign(speedDiff);//calculate the move speed
        player.rb.AddForce(moveAmount*Vector2.right);
        }
        //adding friction
        // if(lastGrounded>0&&Mathf.Abs(player.handler.movement.x)<0.1) {
        //     float temp=Mathf.Min(Mathf.Abs(player.rb.velocity.x),Mathf.Abs(player.pd.horizontalFriction));
        //     temp*=Mathf.Sign(player.rb.velocity.x);
        //     player.rb.AddForce(Vector2.right*-temp,ForceMode2D.Impulse);
        // }
    }
    
}
