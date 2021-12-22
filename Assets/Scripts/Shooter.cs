using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("General settings")]
    public float bulletSpeed = 10f;
    public float bulletDistanceFromPlayer;
    [Header("Prefab settings")]
    public Transform bulletFolder;
    public GameObject bulletPrefab;

    public static List<GameObject> bullets = new List<GameObject>();

    void Update()
    {
        if (Input.GetButtonDown("shoot"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        RemoveOffScreenBullets();
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletFolder);
        Transform playerTransform = transform;
        bullet.transform.position = playerTransform.position + (playerTransform.up * bulletDistanceFromPlayer);
        bullet.transform.rotation = playerTransform.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
        
        bullets.Add(bullet);
    }

    private void RemoveOffScreenBullets()
    {
        foreach (GameObject bullet in bullets.GetRange(0, bullets.Count))
        {
            if (!bullet.GetComponent<SpriteRenderer>().isVisible)
            {
                bullets.Remove(bullet);
            }
        }
    }
}
