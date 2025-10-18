using UnityEngine;
using System.Collections.Generic;



public class TrapSpawner : MonoBehaviour
{
    [Header("Trap Prefabs")]
    public TrapController[] trapPrefabs;

    [Header("Spawn Settings")]
    [Range(0f, 1f)]
    public float spawnChance = 0.8f; // 60% => 80% chance a trap will spawn

    [Header("Spawn Points")]
    [SerializeField] private TrapSpawnPoint[] spawnPoints;

    void Start()
    {
        SpawnTraps();
    }

    void SpawnTraps()
    {
        foreach (TrapSpawnPoint point in spawnPoints)
        {
            if (Random.value > spawnChance) continue;
            
            TrapController trapPrefab = trapPrefabs[Random.Range(0, trapPrefabs.Length)];
            TrapController trapController = Instantiate(trapPrefab, point.transform.position, Quaternion.identity);

            if (trapController != null)
            {
                trapController.SetDirection(point.direction, point.spriteDirection);
            }
        }
    }
}
