using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("General settings")]
    public float bulletSpeed = 10f;
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

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletFolder);
        Transform playerTransform = transform;
        bullet.transform.position = playerTransform.position;
        bullet.transform.rotation = playerTransform.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
    }
}
