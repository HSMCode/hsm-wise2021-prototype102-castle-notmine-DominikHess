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
    


    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource> ();


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
            attempt = 1;
            _as.PlayOneShot(winSound);
            StartCoroutine(WaitForIt(3.0F));
            Instantiate(goalParticle);


        }

        else
        {
            Debug.Log("Attempt " + attempt + " failed! Restarting the game...");
            attempt += 1;
            ReloadingScene();
        }
    }

    IEnumerator WaitForIt(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("SecondLevel");
    }

    void ReloadingScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
