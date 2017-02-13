using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour
{
    //hadling the left over particles
    private void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }

}