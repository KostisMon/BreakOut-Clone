using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField]
    private float ballForce = 500f;
    [SerializeField]
    private GameObject hitParticle;
    [SerializeField]
    private AudioClip ballClip, brickClip;
    private Vector3 direction;
    private AudioSource audioSrc;
    private bool ballInPlay;
    public static Ball instance;
    private static bool isCharged = false;

    // Use this for initialization
    void Start () {
        GetComponentInChildren<ParticleSystem>().Stop(); //there was a small delay stoping the particle effect in ball so I force stop it
        // assinging the instance of Ball
        if (instance == null)                            
        {
            instance = this;
        }
        //assinging the audiosource
        audioSrc = GetComponent<AudioSource>();
        
        //creating a random direction for the ball to fly to
        float randomDirection = Random.Range(-1.0f, 1.0f);
        int x = (int)Mathf.Sign(randomDirection);
        direction = new Vector3(x, 1, 0);
        direction.Normalize();
       
	}
	

	void Update () {
        // press space and the ball flies
        if (Input.GetKeyDown(KeyCode.Space) && ballInPlay == false)
        {
            transform.parent = null;        // un childing it from the paddle
            ballInPlay = true;
            GetComponent<Rigidbody2D>().velocity = direction * ballForce; 
        }

        // starting and stoping the Fireball Power up  particle effect
        if (isCharged)
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }else
        {
            GetComponentInChildren<ParticleSystem>().Stop();
        }


    }



    float hitFactor(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth)
    {
        //returns a float in order to give some more control on where the ball will bounce
        return (ballPosition.x - paddlePosition.x) / paddleWidth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(hitParticle, transform.position, Quaternion.identity);   // create particle on hit
        
        //apply shake effect on the camera when colliding with bricks       
        if(collision.gameObject.tag == "Brick")
        {
            Camera.main.GetComponent<CameraShake>().ShakeCamera(3f, 0.002f);
            audioSrc.PlayOneShot(brickClip);
        }
        else 
        {
            // depending on when the ball hit the paddle it flies to some direction
            audioSrc.PlayOneShot(ballClip);
            if (collision.gameObject.tag == "Paddle") { 
                //Check where on the paddle it hit
                float x = hitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);

                //Calculate the direction
                Vector2 direction = new Vector2(x, 1).normalized;

                //Set the velocity of the ball
                GetComponent<Rigidbody2D>().velocity = direction * ballForce;
            }

        }
    }

  
    //funtions for enabling and disabling the charged effect of the fireball powerup
    public static void FireBallON()
    {
        isCharged = true;
        
    }

    public static void FireBallOFF()
    {
        isCharged = false;

    }
}
