using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupPrefab;

    public void SpawnPowerup(Vector3 pos)
    {
        GameObject powerup = Instantiate(powerupPrefab, transform);
        powerup.transform.position = pos;
    }
}
