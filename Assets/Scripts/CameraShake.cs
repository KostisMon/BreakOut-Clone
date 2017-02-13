using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public bool debugMode = false;//Test-run/Call ShakeCamera() on start

    private float shakeDuration, shakeAmount;

    private float shakePercentage;
    private float startAmount;
    private float startDuration;

    private bool isRunning = false; // for checking the co-routine


    private void Start()
    {

        if (debugMode) ShakeCamera();

    }

    void ShakeCamera()
    {
        startAmount = shakeAmount; // setting default values
        startDuration = shakeDuration;

        if (!isRunning)
        {
            StartCoroutine(Shake()); //Call only if it is not running;
        }
    }

    public void ShakeCamera(float ammount, float duration)
    {
        shakeAmount = ammount; // add extra shakiness
        startAmount = shakeAmount; //Reset the starting amount to get the percentage
        shakeDuration = duration; //add extra duration
        startDuration = shakeDuration; // reset the starting time

        if (!isRunning)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        isRunning = true;

        while(shakeDuration> 0.001f)
        {
            Vector2 rotationAmount = Random.insideUnitCircle * shakeAmount; // Adding rotation

            shakePercentage = shakeDuration / startDuration; // Setting the % of shake
            shakeAmount = startAmount * shakePercentage; //set the amount of shake
            shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime/2); // Lerping the time so it is more smooth towards the end


            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * 5f);//smoothen the local rotation

            yield return null;
        }
        transform.localRotation = Quaternion.identity; // When finished, set the rotation to 0
        isRunning = false;
    }
   

}
