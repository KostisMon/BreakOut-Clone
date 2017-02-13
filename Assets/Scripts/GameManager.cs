using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private int lives = 3;
    [HideInInspector]
    public int balls = 1;
    
    [SerializeField]
    private AudioClip powerUpClip, powerUpRevClip, brickExplosionClip, paddleDeathClip,LoseClip,startingGameClip, winClip, buttonClip;
    [SerializeField]
    private GameObject youWon, gameOver,paddle, deathParticles,ball, panel, backButton;
    [SerializeField]
    private Text livesText;

    private bool didWin = false, gotBricks = false;
    private float resetDelay = 1f;
    private int bricks;
    private GameObject clonePaddle;
    private AudioSource audioSrc;

    private void Start()
    {
        panel.SetActive(true);
        backButton.SetActive(true);
        
        audioSrc = GetComponent<AudioSource>();
        audioSrc.PlayOneShot(startingGameClip);
        
        gameOver.SetActive(false);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        
    }

    public void Setup()
    {
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;  //creates the paddle+ ball 
        GetNumberOfBricks();            // gets the number of bricks in order to check later for completion

    }

    public void CheckGameOver()
    {


        if (bricks < 1)
        {
   
            youWon.SetActive(true);             // setting the won animation on
            didWin = true;                  
            Destroy(livesText);                 //destroys the lives text
            audioSrc.PlayOneShot(winClip);      //plays the win sound
            Time.timeScale = 0.25f;             //slowmo effect
            Invoke("Reset", resetDelay);        //calls the reset functio after some time
        }
        if (lives < 1)
        {
            // he I do the opposite for losing
            if (didWin == false)
            {
                audioSrc.PlayOneShot(LoseClip); 
                gameOver.SetActive(true);
                Destroy(livesText);
                Time.timeScale = 0.25f;
                Invoke("Reset", resetDelay);
            }
        }
    }

    //gets the number of bricks from the level manager
    private void GetNumberOfBricks()
    {
        if (gotBricks == false)
        {
            bricks = LevelManager.instance.numberOfBricks;
            gotBricks = true;

        }

    }

    //resets the game, returning to the menu
    private void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }

    //losing life , checks for how many balls are in game and if less than one subtracks a life
    public void LoseLife()
    {
        balls = balls - 1;

        if (balls <1)
        {
            lives--;
            livesText.text = "Lives: " + lives;         //updates the UI
            Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);       // instantiates the death particles
            audioSrc.PlayOneShot(paddleDeathClip);                                                  // and plays the death sound
            Destroy(clonePaddle);                                                                       
            

            if (lives > 0)
            {

                Invoke("SetupPaddle", resetDelay);

            }
            
            CheckGameOver();
        }

    }
    // deactivates the fireball effect if any and creates the paddle and ball again
    private void SetupPaddle()
    {
        Bricks.ChargedOFF();
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
        clonePaddle.GetComponentInChildren<ParticleSystem>().Stop();
        balls = 1;
    }

    //when a brick is destroyed this is called, reduces the active bricks and checks the game status
    public void ReduceBricks()
    {
        bricks--;
        CheckGameOver();
    }

    // handling the double ball effect. increases the numner of balls and creates a new one
    public void IncreaseBalls()
    {
        balls = balls + 1;

        ball = Instantiate(ball, paddle.transform.position + new Vector3 (0,40,0),Quaternion.identity);
        float randomDirection = Random.Range(-1.0f, 1.0f);
        int x = (int)Mathf.Sign(randomDirection);
        Vector3 direction = new Vector3(x, 1, 0);
        direction.Normalize();
        ball.GetComponent<Rigidbody2D>().velocity = direction * 300f ;

    }
    
    // handling the sounds of the game
    public void PlayPowerUpSound()
    {
        audioSrc.PlayOneShot(powerUpClip);
    }
    public void PlayBrickExplosionSound()
    {
        audioSrc.PlayOneShot(brickExplosionClip);
    }
    public void PlayReversePowerUpSound()
    {
        audioSrc.PlayOneShot(powerUpRevClip);
    }

    // the instrunctions button function, deactivate some ui elements and calls the setup function
    public void GoBack()
    {
        audioSrc.PlayOneShot(buttonClip);
        Setup();
        panel.SetActive(false);
        backButton.SetActive(false);
    }
    

}
