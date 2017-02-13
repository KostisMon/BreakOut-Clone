using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    
    private float paddleSpeed = 10;
    private Vector2 paddlePosition = new Vector2(0, -283);
    private Animator animator;
    public static Paddle instance;
    [SerializeField]
    private GameObject powerUpParticle;
    private bool expanded = false, shrinked =false ;


    
    void Start () {
        animator = GetComponent<Animator>();
        if (instance == null)
        {
            instance = this;
        }
    }
	
	
	void FixedUpdate () {
        // Get the horizontal input
        float h = Input.GetAxisRaw("Horizontal");
        float xPos = transform.position.x + (h * paddleSpeed);

        //moving the paddle left and right between specific points
        paddlePosition = new Vector2(Mathf.Clamp(xPos, -392, 390), paddlePosition.y);
        transform.position = paddlePosition;
        
        
    }

    // expanding the paddle when expand power up is on
    public void ExpandPaddle()
    {
        if (expanded == false)
        {
            expanded = true;
            PlayPowerUpParticles();                     // playing the powerup particles
            GameManager.instance.PlayPowerUpSound();    // playing the power up clip
            animator.SetTrigger("Expand");              //the expanding is handled with an animation
            Invoke("ReturnFromExpand", 4f);             // after 4 seconds the paddle returns to its original scale
        }
       
    }

    //shrinkingg the paddle, it is the opposite of ExpandPaddle
    public void ShrinkPaddle()
    {
        if (shrinked == false)
        {
            shrinked = true;
            PlayPowerUpParticles();
            GameManager.instance.PlayReversePowerUpSound();
            animator.SetTrigger("Shrink");
            Invoke("ReturnFromShrink", 4f);
        }

    }

    
    // handling expanding  through animation
    public void ReturnFromExpand()
    {

        GameManager.instance.PlayReversePowerUpSound();
        animator.SetTrigger("ReturnFromExpand");
        expanded = false;
    }

    public void ReturnFromShrink()
    {
        GameManager.instance.PlayPowerUpSound();
        animator.SetTrigger("ReturnFromShrink");
        shrinked = false;
    }
    
    // plays the powerup particles
    public void PlayPowerUpParticles()
    {
        Instantiate(powerUpParticle, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach(ContactPoint contact in collision.contacts)
        {
            if(contact.thisCollider == GetComponent<BoxCollider2D>())
            {
                float calc = contact.point.x - transform.position.x;
                contact.otherCollider.GetComponent<Rigidbody2D>().AddForce(new Vector2(200f*calc,0));
            }
        }

        
    }
}
