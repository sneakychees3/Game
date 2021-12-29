using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAfterImagePool : MonoBehaviour
{
   [SerializeField]
   private GameObject afterImage;
   
   private Queue<GameObject> availableObjects=new Queue<GameObject>();
   public static playerAfterImagePool Instance{get;private set;}

   private void Awake(){
       Instance=this;
       growPool();
   }
   private void growPool(){
       for(int i=0;i<11;i++){
        var instanceToAdd=Instantiate(afterImage);
        instanceToAdd.transform.SetParent(transform);
        AddToPool(instanceToAdd);
       }
   }
   public void AddToPool(GameObject a){
    a.SetActive(false);
    availableObjects.Enqueue(a);
   }

   public GameObject getFromPool(){
       if(availableObjects.Count==0){
           growPool();
       }
       var Instance=availableObjects.Dequeue();
       Instance.SetActive(true);
       return Instance;
   }
}
