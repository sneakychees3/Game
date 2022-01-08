using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAfterImageSprite : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer sr;
    private SpriteRenderer playerSr;
    private Color c;
    
    [SerializeField]private float activeTime=0.1f;
    private float timeActivated;
    private float alpha;
    private float alphaSet=0.8f;
    [SerializeField]private float alphaMultiplier=0.7f;

    private void OnEnable() {
        sr=GetComponent<SpriteRenderer>();
        player=GameObject.FindGameObjectWithTag("Player").transform;
        playerSr=player.GetComponent<SpriteRenderer>();

        alpha=alphaSet;
        sr.sprite=playerSr.sprite;
        transform.position=player.position;
        transform.rotation=player.rotation;
        timeActivated=Time.time;
    }
    private void Update() {
        alpha*=alphaMultiplier;
        c=new Color(1f,1f,1f,alpha);
        sr.color=c;
        if(Time.time>=(timeActivated+activeTime)){
            playerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
