using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class ElfSpawner : MonoBehaviour
{
    public GameObject elfPrefab;
    public Transform elfSpawnFolder;
    public Transform playerTransform;
    public float timeBetweenSpawns = 5f;

    private float timer = 0f;
    private List<Transform> spawns = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        // get list of spawns from spawn folder
        foreach (Transform child in elfSpawnFolder)
        {
            spawns.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // spawn elves every few seconds
        timer += Time.deltaTime;
        if (timer >= timeBetweenSpawns)
        {
            timer = 0f;
            SpawnElf();
        }
    }

    void SpawnElf()
    {
        GameObject elf = Instantiate(elfPrefab, transform);
        // make elf follow the player
        elf.GetComponent<AIDestinationSetter>().target = playerTransform;
        // randomize spawn
        elf.transform.position = spawns[UnityEngine.Random.Range(0, spawns.Count)].position;
    }
}
