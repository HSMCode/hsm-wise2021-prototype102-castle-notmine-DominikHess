using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastErScript : MonoBehaviour
{


    public int speed = 1;
    private bool walkingLeft = false;   //define flag for walk-direction
    
    public GameObject CastingRay;
    private bool CastingFlag = false;

    public AudioSource casterAudioSource;

    private bool stop = false;

    void Start()
    {
        stop = false;
        CastingRay.SetActive(false);
        casterAudioSource.Play();
    }



    void Update()
    {
        if (!stop)
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

    }
   
    private void OnTriggerEnter(Collider other)
    {
           if(other.tag == "CastEl" && !CastingFlag)
       {
           CastingRay.SetActive(true);
           CastingFlag = true;
       }
    }


    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.name == "Cast-el")
        {
            stop = true;
            casterAudioSource.Stop();
        }


        if(collision.gameObject.tag == "Wall")      //if caster walks into a wall, the walk-direction flag is changed
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
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "CastEl")
        {
            CastingRay.SetActive(false);
            CastingFlag = false;
        }
    }


}
