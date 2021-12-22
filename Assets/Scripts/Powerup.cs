using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [Header("General settings")]
    public float duration = 5f;
    [Header("Powerup settings")]
    public float speedBoost = 5f;
    public float gattlingGunSpeed = 0f;

    private List<Func<Collider2D, IEnumerator>> powerups = new List<Func<Collider2D, IEnumerator>>();
    
    private void Start()
    {
        //powerups.Add(SpeedBoost);
        //powerups.Add(Shotgun);
        //powerups.Add(GattlingGun);
        powerups.Add(FreezeElves);
        //powerups.Add(QuadShot);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        Func<Collider2D, IEnumerator> powerup = powerups[UnityEngine.Random.Range(0, powerups.Count)];
        
        StartCoroutine(powerup(player));
    }

    IEnumerator SpeedBoost(Collider2D player)
    {
        MovementController movementController = player.GetComponent<MovementController>();
        movementController.speed += speedBoost;
        movementController.iceSpeed += speedBoost;

        yield return new WaitForSeconds(duration);

        movementController.speed -= speedBoost;
        movementController.iceSpeed -= speedBoost;
        
        Destroy(gameObject);
    }

    IEnumerator Shotgun(Collider2D player)
    {
        Shooter shooter = player.GetComponent<Shooter>();
        shooter.triShot = true;

        yield return new WaitForSeconds(duration);

        shooter.triShot = false;
        
        Destroy(gameObject);
    }

    IEnumerator GattlingGun(Collider2D player)
    {
        Shooter shooter = player.GetComponent<Shooter>();
        shooter.shootCooldown = 0;

        yield return new WaitForSeconds(duration);

        shooter.shootCooldown = shooter.normalShootCoolDown;

        Destroy(gameObject);
    }

    IEnumerator FreezeElves(Collider2D player)
    {
        ElfSpawner.frozen = true;
        foreach (GameObject elf in ElfSpawner.elves)
        {
            elf.GetComponent<Elf>().Freeze();
        }

        yield return new WaitForSeconds(duration);

        ElfSpawner.frozen = false;
        foreach (GameObject elf in ElfSpawner.elves)
        {
            elf.GetComponent<Elf>().Unfreeze();
        }
        
        Destroy(gameObject);
    }

    IEnumerator QuadShot(Collider2D player)
    {
        Shooter shooter = player.GetComponent<Shooter>();
        shooter.quadShot = true;

        yield return new WaitForSeconds(duration);

        shooter.quadShot = false;
        
        Destroy(gameObject);
    }
}
