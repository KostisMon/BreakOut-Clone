using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallPowerUp : MonoBehaviour {

    // setting a constant dropspeed and making it fall downwards
    private float dropspeed = 3;
    
 
    private void Update()
    {

        transform.position = transform.position + (Vector3.down * dropspeed);

    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball.FireBallON();                                  // enabling the particles for fireball
        Bricks.ChargedON();                                 // changing a bool in order to make all brick colliders triggers
        GameManager.instance.PlayPowerUpSound();            // playing the powerup clip
        Paddle.instance.PlayPowerUpParticles();             // playing the particles
        
        GetComponent<SpriteRenderer>().enabled = false;     
        Invoke("TurnChargedOFF", 4f);                       // after 4 seconds calling the opposite functions in order to end the effect

    }
    
    // calling the oposite functions
    private void TurnChargedOFF()
    {
        Ball.FireBallOFF();
        Bricks.ChargedOFF();
        GameManager.instance.PlayReversePowerUpSound();
        Destroy(gameObject);
    }
}
