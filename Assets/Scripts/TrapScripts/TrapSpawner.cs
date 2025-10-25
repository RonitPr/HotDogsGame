using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TrapSpawner : MonoBehaviour
{
    [Header("Trap Prefabs")]
    public TrapController[] trapPrefabs;

    [Header("Spawn Settings")]
    [Range(0f, 1f)]
    public float spawnChance = 0.8f; // 60% => 80% chance a trap will spawn

    [Header("Spawn Points")]
    [SerializeField] private TrapSpawnPoint[] spawnPoints;

    private static int _spawnCounter;
    public static int SpawnCounter => _spawnCounter;

    private void OnEnable()
    {
        GameManager.OnGameEnd += HandleTrapCounterReset;
    }

    private void OnDisable()
    {
        GameManager.OnGameEnd -= HandleTrapCounterReset;
    }

    void Start()
    {
        SpawnTraps();
        UIManager.Instance.UpdateTrapCounter(TrapController.DefeatedCounter, SpawnCounter);
    }

    void SpawnTraps()
    {
        foreach (TrapSpawnPoint point in spawnPoints)
        {
            if (Random.value > spawnChance)
            {
                continue;
            }
            _spawnCounter++;

            TrapController trapPrefab = trapPrefabs[Random.Range(0, trapPrefabs.Length)];
            TrapController trapController = Instantiate(trapPrefab, point.transform.position, Quaternion.identity);

            if (trapController != null)
            {
                trapController.SetDirection(point.direction, point.spriteDirection);
            }
        }
    }
    private void HandleTrapCounterReset()
    {
        _spawnCounter = 0;
    }
}
