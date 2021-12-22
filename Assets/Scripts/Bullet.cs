using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Elf"))
        {
<<<<<<< HEAD
            ScoreScript.scoreValue ++;
            ElfSpawner.elves.Remove(other.gameObject);
            Destroy(other.gameObject);
=======
            other.GetComponent<Elf>().Die();
>>>>>>> db37d67366c2b636778314cb01ee083fedcf6b2b
            Destroy(gameObject);

        }
    }

    // delete the bullet after it goes offscreen
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 2f);
    }
}
