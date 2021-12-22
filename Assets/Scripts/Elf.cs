using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : MonoBehaviour
{
    public void Die()
    {
        ScoreScript.scoreValue++;
        
        // chance to spawn powerup
        if (Random.Range(0, 5) == 0)
        {
            FindObjectOfType<PowerupSpawner>().SpawnPowerup(transform.position);
        }

        Destroy(gameObject);
    }
}
