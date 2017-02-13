using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBallPowerUp : MonoBehaviour {


    private float dropspeed = 3;


    private void Update()
    {

        transform.position = transform.position + (Vector3.down * dropspeed);

    }

    // calling the neccessary functions for the doubling of the balls.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.IncreaseBalls();
        GameManager.instance.PlayPowerUpSound();
        Paddle.instance.PlayPowerUpParticles();
        Destroy(gameObject);

    }

}
