using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {

    [SerializeField]
    private char brickType;
    [SerializeField]
    private GameObject  brickDestructionParticle;
    [SerializeField]
    private Sprite[] stateSprites;
    [SerializeField]
    private GameObject[] powerUps;

    private static bool isCharged=false;
    public static Bricks instance;
    private SpriteRenderer spriteRenderer;
    private int currentSprite = -1;
    private bool deleting;
    private Animator animator;
    private int arrayLength;
    private bool triggerOn =false;


    private void Start()
    {
 
        if (instance == null)
        {
            instance = this;
        }
        spriteRenderer = GetComponent<SpriteRenderer>(); //  accessing the SpriteRenderer that is attached to the Gameobject
        arrayLength = stateSprites.Length -1;
        animator = GetComponent<Animator>();

    }

    private void Update()
    {

        // setting the colliders as triggers for the fireball power up
        if (isCharged == true && triggerOn == false)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision only works when fireball power up is off
        if (isCharged == false)
        {
            CheckState();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //triggers work when fireball powerup is on
        if (isCharged)
        {
            DestroyBrick();
        }
       
    }
    //checking the state of the brick
    private void CheckState()
    {
        currentSprite++;
        if (currentSprite > arrayLength)
        {
            //if the sprite corresponds to the last state of the brick , calls the destroy function
            DestroyBrick();
                  

        }
        if (!deleting)
        {
            //else it changes the state/sprite
            //play hit animation
            animator.SetTrigger("Hit");

            spriteRenderer.sprite = stateSprites[currentSprite];

        }

    }
    
    // function taking care of the destruction of the bricks
    private void DestroyBrick()
    {
        deleting = true;
        Destroy(gameObject);
        //Play particle
        Instantiate(brickDestructionParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
        if (brickType == 'x')
        {
            Instantiate(powerUps[Random.Range(0, 4)], new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
        }
        //Play Sound
        GameManager.instance.PlayBrickExplosionSound();
        //Destroy the brick
        GameManager.instance.ReduceBricks();
        
    }

    // functions assisting the fireball power up
    public static void ChargedON()
    {
        isCharged = true;
        

    }

    public static void ChargedOFF()
    {
        isCharged = false;
        
    }


}
