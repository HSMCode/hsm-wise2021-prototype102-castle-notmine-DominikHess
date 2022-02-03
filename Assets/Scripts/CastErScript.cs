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
        stop = false;       //stop-bool is used to disable walking on certain collisions
        CastingRay.SetActive(false);        //particle-system is deactivated
        casterAudioSource.Play();       //music is started
    }



    void Update()
    {
        if (!stop)      //while stop-bool is false, walking is enabled
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
   
    private void OnTriggerEnter(Collider other)     //if castel is hit, the particle-system is started and a flag set to true to keep the particle system from restarting every frame
    {
           if(other.tag == "CastEl" && !CastingFlag)
       {
           CastingRay.SetActive(true);
           CastingFlag = true;
       }
    }


    void OnCollisionEnter(Collision collision)      
    {

        if(collision.gameObject.name == "Cast-el")      //if caster collides with castel:
        {
            stop = true;        //walking is disabled
            casterAudioSource.Stop();       //music is stopped (to give room for the insult played by the castel-script)
        }


        if(collision.gameObject.tag == "Wall")      //if caster walks into a wall, the walk-direction flag is changed
       { 
           if(walkingLeft)      //if caster was walking left previously, flag is set to false to make him go up
           {
               walkingLeft = false;
           }
           else if(!walkingLeft)        //if caster was walking up previously, flag is set to true to make him go left
           {
               walkingLeft = true;
           }
       }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "CastEl")       //once both get teleported back to their starting points after level restart, casting is disabled
        {
            CastingRay.SetActive(false);
            CastingFlag = false;
        }
    }


}
