using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {

    //death trigger at the bottom of map, it calls the function responsible for decreasing
    // and checking gamestatus, plus it destroys the ball object
    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameManager.instance.LoseLife();
        Destroy(collision.gameObject);
        

    }

   

    
}
