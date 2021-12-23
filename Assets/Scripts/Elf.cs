using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Elf : MonoBehaviour
{
    private AIPath aiPath;
    private float maxSpeed;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        maxSpeed = UnityEngine.Random.Range(aiPath.maxSpeed - 2f, aiPath.maxSpeed + 2f);
    }

    public void Die()
    {
        ScoreScript.scoreValue++;
        
        // chance to spawn powerup
        if (UnityEngine.Random.Range(0, 5) == 0)
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
