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
            ElfSpawner.elves.Remove(other.gameObject);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    // delete the bullet after it goes offscreen
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 2f);
    }
}
