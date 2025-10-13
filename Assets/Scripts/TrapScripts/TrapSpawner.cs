using UnityEngine;
using System.Collections.Generic;



public class TrapSpawner : MonoBehaviour
{
    //[SerializeField] private List<GameObject> trapPrefabs; // assign 2 trap prefabs
    //[SerializeField, Range(0f, 1f)] private float spawnChance = 0.6f;
    //[SerializeField] private List<Transform> trapSpawnPoints; // assign in Inspector
    
    //public List<GameObject> trapPrefabs; // assign 2 trap prefabs
    
    [Header("Trap Prefabs")]
    public GameObject[] trapPrefabs; // Assign your trap prefabs here (Slime, Spikes, etc.)

    [Header("Spawn Settings")]
    [Range(0f, 1f)]
    public float spawnChance = 0.6f; // 60% chance a trap will spawn

    [Header("Spawn Points")]
    [SerializeField] private TrapSpawnPoint[] spawnPoints; // drag them manually in Inspector

    void Start()
    {
        SpawnTraps();
    }

    void SpawnTraps()
    {
        foreach (TrapSpawnPoint point in spawnPoints)
        {
            //float roll = Random.value;
            //if (roll > spawnChance)
            //{
            //    Debug.Log($"No trap spawned at {point.name} (rolled {roll})");
            //    continue;
            //}
            if (Random.value > spawnChance) continue;
            
            //GameObject randomTrap = trapPrefabs[Random.Range(0, trapPrefabs.Length)];
            //Instantiate(randomTrap, point.transform.position, Quaternion.identity);
            GameObject trapPrefab = trapPrefabs[Random.Range(0, trapPrefabs.Length)];
            GameObject trapGO = Instantiate(trapPrefab, point.transform.position, Quaternion.identity);

            //Debug.Log($"Spawned trap {randomTrap.name} at {point.name}");
            
            TrapController trapController = trapGO.GetComponent<TrapController>();
            if (trapController != null)
            {
                trapController.SetDirection(point.direction);
            }
        }
    }
}
