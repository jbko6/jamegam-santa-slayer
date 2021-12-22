using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("General settings")]
    public float bulletSpeed = 10f;
    public float bulletDistanceFromPlayer;
    public bool triShot;
    public bool quadShot;
    public float normalShootCoolDown = 1f;
    public float shootCooldown = 1f;
    [Header("Prefab settings")]
    public Transform bulletFolder;
    public GameObject bulletPrefab;

    private Transform playerTransform;
    private float shootTimer = 0f;

    private void Start()
    {
        playerTransform = transform;
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        
        if (Input.GetButton("Fire1") && Time.timeScale == 1f)
        {
            if (shootTimer >= shootCooldown)
            {
                shootTimer = 0f;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        ShootAtAngle(0f);

        // shoot two extra bullets
        if (triShot)
        {
            ShootAtAngle(-15f);
            ShootAtAngle(15f);
        }

        if (quadShot)
        {
            ShootAtAngle(90f);
            ShootAtAngle(-90f);
            ShootAtAngle(180f);
        }
    }

    private void ShootAtAngle(float angle)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletFolder);
        bullet.transform.rotation = playerTransform.rotation;
        bullet.transform.Rotate(0, 0, angle);
        bullet.transform.position = playerTransform.position + (bullet.transform.up * bulletDistanceFromPlayer);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
    }
}
