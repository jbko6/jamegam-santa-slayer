using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Elf"))
        {
            other.GetComponent<Elf>().Die();
            
            // snow splatter effect
            GameObject effect = Instantiate(hitEffect);
            effect.transform.position = transform.position;
            
            Destroy(gameObject);
        }
    }

    // delete the bullet after it goes offscreen
    private void OnBecameInvisible()
    {
        // this happens 2 seconds later so that the particles don't get destroyed abruptly on screen
        Destroy(gameObject, 2f);
    }
}
