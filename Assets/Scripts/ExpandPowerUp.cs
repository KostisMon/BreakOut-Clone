﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandPowerUp : MonoBehaviour {
    private float dropspeed = 3;

    private void Update()
    {

        transform.position = transform.position + (Vector3.down * dropspeed);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
        Paddle.instance.ExpandPaddle();

    }
}
