using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPowerUp : MonoBehaviour {

    private float dropspeed = 3;

    private void Update()
    {

        transform.position = transform.position + (Vector3.down * dropspeed);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destroys the power up and calls the function for shrinking
        Destroy(gameObject);
        Paddle.instance.ShrinkPaddle();

    }
}
