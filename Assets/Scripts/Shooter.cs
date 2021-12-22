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
    [Header("Prefab settings")]
    public Transform bulletFolder;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale == 1f)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletFolder);
        Transform playerTransform = transform;
        bullet.transform.position = playerTransform.position + (playerTransform.up * bulletDistanceFromPlayer);
        bullet.transform.rotation = playerTransform.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;

        // shoot two extra bullets
        if (triShot)
        {
            GameObject leftBullet = Instantiate(bulletPrefab, bulletFolder);
            //leftBullet.transform.position = playerTransform.position + (playerTransform.up * bulletDistanceFromPlayer);
            leftBullet.transform.rotation = playerTransform.rotation;
            leftBullet.transform.Rotate(0, 0, -15f);
            leftBullet.transform.position = playerTransform.position + (leftBullet.transform.up * bulletDistanceFromPlayer);
            leftBullet.GetComponent<Rigidbody2D>().velocity = leftBullet.transform.up * bulletSpeed;
            
            GameObject rightBullet = Instantiate(bulletPrefab, bulletFolder);
            rightBullet.transform.rotation = playerTransform.rotation;
            rightBullet.transform.Rotate(0, 0, 15f);
            rightBullet.transform.position = playerTransform.position + (rightBullet.transform.up * bulletDistanceFromPlayer);
            rightBullet.GetComponent<Rigidbody2D>().velocity = rightBullet.transform.up * bulletSpeed;
        }
    }
}
