using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CastElScript : MonoBehaviour
{

    public int speed = 1;
    public int attempt = 1;
    Dictionary<string, Vector3> dict = new Dictionary<string, Vector3>();
    public ParticleSystem goalParticle;
    public AudioClip winSound;
    public AudioSource _as;
    public AudioClip[] insults;

    private bool stop = false;
    


    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource> ();     
        stop = false;       //stop-bool is used in OnCollisionEnter if anything is hit


    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)      //while the stop-bool is false, castel walks right unless space is held down, then he walks left
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
    }


    void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.name == "Goal")     //if castel collides with the goal:
        {
            Debug.Log("You win! It took you " + attempt + " attempt(s)!");      //console tells the player that the´ve won
            attempt = 1;        //number of attempts is reset to one
            _as.PlayOneShot(winSound);      //winsound is played
            StartCoroutine(WaitForIt(3.0F));        //a short wait-time is started
            Instantiate(goalParticle);      //a particle-system is rendered
            stop = true;        //walking is disabled


        }

        else
        {
            Debug.Log("Attempt " + attempt + " failed! Restarting the game...");        //if castel collides with a wall or the caster:
            attempt += 1;       //no. of attempts is increased
            StartCoroutine(Restarter(3.0F));        //restart-coroutine is initiated (see below)
            stop = true;        //walking is disabled
            if(collision.gameObject.name == "Cast-er")      //if castel specifically collides with caster:
            {
                int randomInsult = Random.Range(0, insults.Length);     //a random insult-sound is played
                _as.PlayOneShot(insults[randomInsult]);
            }
        }
    }

    IEnumerator WaitForIt(float waitTime)       //this waits 3 seconds and then loads the next level to give the player time to process their win
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("SecondLevel");
    }

    IEnumerator Restarter(float waitTime)       //this waits 3 seconds and then restarts the game to give the player time to process their loss
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);;
    }

}
