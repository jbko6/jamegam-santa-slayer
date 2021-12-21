using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("General settings")]
    public float bulletSpeed = 10f;
    [Header("Player settings")]
    public MovementController playerMovementController;
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
        bullet.transform.position = playerTransform.position;
        bullet.transform.rotation = playerTransform.rotation;

        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = playerMovementController.direction;
        bulletRigidbody.velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed);
        
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
