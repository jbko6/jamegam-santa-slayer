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
    public bool blizzard;
    public bool noCooldown;
    public float normalShootCoolDown = 1f;
    public float shootCooldown = 1f;
    public float blizzardShootCooldown = 0.05f;
    [Header("Prefab settings")]
    public Transform bulletFolder;
    public GameObject bulletPrefab;
    
    private Transform playerTransform;
    private float shootTimer = 0f;
    private float blizzardShootTimer = 0f;
    private float blizzardAngle = 0f;

    private void Start()
    {
        playerTransform = transform;
    }

    void Update()
    {
        shootTimer += Time.deltaTime;
        
        if (Input.GetButton("Fire1") && Time.timeScale == 1f)
        {
            if (shootTimer >= shootCooldown || noCooldown)
            {
                shootTimer = 0f;
                Shoot();
            }
        }

        if (blizzard)
        {
            blizzardShootTimer += Time.deltaTime;

            if (blizzardShootTimer >= blizzardShootCooldown)
            {
                blizzardShootTimer = 0f;
                ShootAtAngle(blizzardAngle);
                blizzardAngle += 30f;
            }
        }
    }

    private void Shoot()
    {
        if (noCooldown)
        {
            ShootAtAngle(UnityEngine.Random.Range(-25f, 25f));
        }
        else
        {
            ShootAtAngle(0f);
        }

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

    public void ResetBlizzard()
    {
        blizzardShootTimer = 0f;
        blizzardAngle = 0f;
    }
}
