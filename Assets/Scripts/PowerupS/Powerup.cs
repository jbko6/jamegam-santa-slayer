using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : MonoBehaviour
{
    [Header("General settings")]
    public float duration = 5f;
    public float timeToCollect = 10f;
    [Header("Powerup settings")]
    public float speedBoost = 5f;
    public float gattlingGunSpeed = 0f;
    public Sprite speedImage;
    public Sprite shotgunImage;
    public Sprite gattlingGunImage;
    public Sprite freezeImage;
    public Sprite quadShotImage;
    public Sprite blizzardImage;
    public GameObject powerupPrefab;

    private List<GameObject> powerupUIElements = new List<GameObject>();
    private List<Func<Collider2D, IEnumerator>> powerups = new List<Func<Collider2D, IEnumerator>>();
    private float timer = 0f;
    private bool pickedUp = false;
    
    private void Start()
    {
        powerups.Add(SpeedBoost);
        powerups.Add(Shotgun);
        powerups.Add(GattlingGun);
        powerups.Add(FreezeElves);
        powerups.Add(QuadShot);
        powerups.Add(Blizzard);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToCollect && !pickedUp)
        {
            PowerupSpawner.powerups.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    public GameObject createImage(Sprite powerupSprite) {
        GameObject powerup = Instantiate(powerupPrefab, GameObject.Find("/Canvas").transform);
        Image powerupImage = powerup.GetComponent<Image>();
        powerupImage.sprite = powerupSprite;
        RectTransform rect = powerup.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(PowerupSpawner.powerupUIElements.Count * 60, 15);
        Debug.Log(PowerupSpawner.powerupUIElements.Count);
        PowerupSpawner.powerupUIElements.Add(powerup);
        return powerup;
    }

    public void removeImage() {
        Destroy(PowerupSpawner.powerupUIElements[0]);
        PowerupSpawner.powerupUIElements.Remove(PowerupSpawner.powerupUIElements[0]);
        foreach (GameObject image in PowerupSpawner.powerupUIElements) {
            image.GetComponent<RectTransform>().anchoredPosition = new Vector2(image.GetComponent<RectTransform>().anchoredPosition.x - 60, 15);
        }
    }

    void Pickup(Collider2D player)
    {
        pickedUp = true;
        
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        Func<Collider2D, IEnumerator> powerup = powerups[UnityEngine.Random.Range(0, powerups.Count)];
        
        StartCoroutine(powerup(player));

        PowerupSpawner.powerups.Remove(gameObject);
    }

    IEnumerator SpeedBoost(Collider2D player)
    {
        MovementController movementController = player.GetComponent<MovementController>();
        movementController.speed += speedBoost;
        movementController.iceSpeed += speedBoost;

        createImage(speedImage);

        yield return new WaitForSeconds(duration);

        removeImage();

        movementController.speed -= speedBoost;
        movementController.iceSpeed -= speedBoost;
        
        Destroy(gameObject);
    }

    IEnumerator Shotgun(Collider2D player)
    {
        Shooter shooter = player.GetComponent<Shooter>();
        shooter.triShot = true;

        createImage(shotgunImage);

        yield return new WaitForSeconds(duration);

        removeImage();

        shooter.triShot = false;
        
        Destroy(gameObject);
    }

    IEnumerator GattlingGun(Collider2D player)
    {
        Shooter shooter = player.GetComponent<Shooter>();
        shooter.noCooldown = true;

        createImage(gattlingGunImage);

        yield return new WaitForSeconds(duration);

        removeImage();

        shooter.noCooldown = false;

        Destroy(gameObject);
    }

    IEnumerator FreezeElves(Collider2D player)
    {
        ElfSpawner.frozen = true;
        foreach (GameObject elf in ElfSpawner.elves)
        {
            elf.GetComponent<Elf>().Freeze();
        }

        createImage(freezeImage);

        yield return new WaitForSeconds(duration);

        removeImage();

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

        createImage(quadShotImage);

        yield return new WaitForSeconds(duration);

        removeImage();

        shooter.quadShot = false;
        
        Destroy(gameObject);
    }

    IEnumerator Blizzard(Collider2D player)
    {
        Shooter shooter = player.GetComponent<Shooter>();
        shooter.ResetBlizzard();
        shooter.blizzard = true;

        createImage(blizzardImage);

        yield return new WaitForSeconds(duration);

        removeImage();

        shooter.blizzard = false;

        Destroy(gameObject);
    }
}
