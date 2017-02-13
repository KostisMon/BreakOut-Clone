using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour {

    Vector3 currScale, targetScale;
    private bool timeToScaleUp = false;
    private bool timeToScaleDown = true;

    /// <summary>
    /// because of the rescaling of the bricks, my animation fiddling with a breath in - breath out effect couldnt play
    /// so he it is done programatically
    /// </summary>
  


    private IEnumerator ScaleDown(float time)
    {
        float originalTime = time;

        while(time > 0.0f)
        {
            time -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale, currScale, time / originalTime); // scaling the object down
            
            
            yield return null;
           
        }

        timeToScaleUp = true;

    }
    private IEnumerator ScaleUp(float time)
    {
        float originalTime = time;

        while (time > 0.0f)
        {
            time -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(currScale, targetScale, time / originalTime); //scaling it up to its original scale


            yield return null;
         
        }
      
        timeToScaleDown = true;
    }
    private void Start()
    {
        currScale = transform.localScale;
        targetScale = currScale * 0.9f;
       
        
    }
    private void Update()
    {
        if (timeToScaleDown)
        {
            StartCoroutine(ScaleDown(0.5f));
            timeToScaleDown = false;
            
        }
        if (timeToScaleUp)
        {
            StartCoroutine(ScaleUp(1.0f));
            timeToScaleUp = false;
        }
    }
}
