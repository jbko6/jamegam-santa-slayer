using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class Elf : MonoBehaviour
{
    public AIPath aiPath;

    private float maxSpeed;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        maxSpeed = aiPath.maxSpeed;
    }

    public void Die()
    {
        ScoreScript.scoreValue++;
        
        // chance to spawn powerup
        if (Random.Range(0, 5) == 0)
        {
            FindObjectOfType<PowerupSpawner>().SpawnPowerup(transform.position);
        }

        ElfSpawner.elves.Remove(gameObject);

        Destroy(gameObject);
    }

    public void Freeze()
    {
        aiPath.maxSpeed = 0f;
    }

    public void Unfreeze()
    {
        aiPath.maxSpeed = maxSpeed;
    }
}
