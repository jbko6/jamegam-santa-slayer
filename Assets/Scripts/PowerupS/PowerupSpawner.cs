using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupPrefab;
    public int maxPowerups = 6;

    public static List<GameObject> powerups = new List<GameObject>();
    public static List<GameObject> powerupUIElements = new List<GameObject>();

    public void SpawnPowerup(Vector3 pos)
    {
        if (powerups.Count < maxPowerups)
        {
            GameObject powerup = Instantiate(powerupPrefab, transform);
            powerup.transform.position = pos;
            powerups.Add(powerup);
        }
    }
}
