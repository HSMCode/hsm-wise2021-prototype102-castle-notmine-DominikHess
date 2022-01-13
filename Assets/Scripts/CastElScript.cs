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
        stop = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
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

        if(collision.gameObject.name == "Goal")
        {
            Debug.Log("You win! It took you " + attempt + " attempt(s)!");
            attempt = 1;
            _as.PlayOneShot(winSound);
            StartCoroutine(WaitForIt(3.0F));
            Instantiate(goalParticle);
            stop = true;


        }

        else
        {
            Debug.Log("Attempt " + attempt + " failed! Restarting the game...");
            attempt += 1;
            StartCoroutine(Restarter(2.0F));
            stop = true;
            if(collision.gameObject.name == "Cast-er")
            {
                int randomInsult = Random.Range(0, insults.Length);
                _as.PlayOneShot(insults[randomInsult]);
            }
        }
    }

    IEnumerator WaitForIt(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("SecondLevel");
    }

    IEnumerator Restarter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);;
    }

}
