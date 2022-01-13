using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastErScript : MonoBehaviour
{


    public int speed = 1;
    private bool walkingLeft = false;   //define flag for walk-direction
    
    public GameObject CastingRay;
    private bool CastingFlag = false;

    void Start()
    {
      

    }



    void Update()
    {
        if(walkingLeft)     //depending on state of walkdirection flag, either walk left or up, state of the flag is set below on collision
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed); 
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
   
    
   private void OnTriggerEnter(Collider other)   
   {    
       //Debug.Log("hit");
       if(other.tag == "Wall")      //if caster walks into a wall, the walk-direction flag is changed
       {
           if(walkingLeft)
           {
               walkingLeft = false;
           }
           else if(!walkingLeft)
           {
               walkingLeft = true;
           }
       }

        if(other.tag == "CastEl" && !CastingFlag)
       {
           Instantiate(CastingRay, transform);
           CastingFlag = true;
       }
   }

}
