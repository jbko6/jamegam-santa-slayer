using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public List<AudioClip> hitSounds;

    private AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = hitSounds[UnityEngine.Random.Range(0, hitSounds.Count)];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Elf"))
        {
            other.GetComponent<Elf>().Die();
            
            PlayHitEffects();
            
            Destroy(gameObject, audioSource.clip.length);
        } else if (other.CompareTag("Snowman"))
        {
            other.GetComponent<Snowman>().Die();
            
            PlayHitEffects();
            
            Destroy(gameObject, audioSource.clip.length);
        }
    }

    private void PlayHitEffects()
    {
        // snow splatter effect
        GameObject effect = Instantiate(hitEffect);
        effect.transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
            
        // play hit sound
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(GetComponent<ParticleSystem>());
            
        audioSource.Play();
    }

    // delete the bullet after it goes offscreen
    private void OnBecameInvisible()
    {
        // this happens 1 second later so that the particles don't get destroyed abruptly on screen
        Destroy(gameObject, 1f);
    }
}
