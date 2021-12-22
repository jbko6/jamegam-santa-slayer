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
            effect.transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
            
            Destroy(gameObject);
        }
    }

    // delete the bullet after it goes offscreen
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 1f);
    }
}
