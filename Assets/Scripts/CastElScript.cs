using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastElScript : MonoBehaviour
{

    public int speed = 1;
    public int attempt = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);

        } 
    }


    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.name == "Goal")
        {
            Debug.Log("You win! It took you " + attempt + " attempt(s)!");
            Debug.Log("Restarting the game...");
            attempt = 1;
            transform.position = new Vector3(0f, 0f, 0f); // Reset Game by putting Cast-el back to the start

        }

        else
        {
            Debug.Log("Attempt " + attempt + " failed! Restarting the game...");
            attempt += 1;
            transform.position = new Vector3(0f, 0f, 0f); // Reset Game by putting Cast-el back to the start
        }
    }


}
