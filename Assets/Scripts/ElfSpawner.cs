using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class ElfSpawner : MonoBehaviour
{
    public GameObject elfPrefab;
    public Transform playerTransform;
    public float timeBetweenSpawns = 5f;

    private float timer = 0f;
    private List<GameObject> elves = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        elf.GetComponent<AIDestinationSetter>().target = playerTransform;
    }
}
