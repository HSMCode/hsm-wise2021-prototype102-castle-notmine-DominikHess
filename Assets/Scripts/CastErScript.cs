using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastErScript : MonoBehaviour
{


    public int speed = 1;





    void Start()
    {
      

    }



    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed); //Cast-Er automaticly goes to the left
   
    }
   
    
   
    // Cast-Er goes up when colliding with the Wall
    private void OnCollisionStay(Collision collision)
    {
       if (collision.gameObject.CompareTag ("Wall"))
       {
          transform.Translate(Vector3.forward * Time.deltaTime * speed);
       }

   
    }
}
