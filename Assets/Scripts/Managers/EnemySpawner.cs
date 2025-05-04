using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform player;

    [Header("Attributes")]
    [SerializeField] private float maxSpawnRate;
    [SerializeField] private float minSpawnRate;
    [SerializeField] private int enemysPer;
    [SerializeField] private float spawnRadius;

    [Header("Limits")]
    [SerializeField] private float minDistance;
    [SerializeField] private int maxEnemies;
    [SerializeField] private int currentEnemies;

    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Random.Range(minSpawnRate, maxSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        // Prevents spawing when time is stopped
        if (TimeManager.instance.IsTimeStopped())
        {
            return;
        }

        spawnTimer -= Time.deltaTime;

        // Check if the spawn timer has reached zero
        if (spawnTimer <= 0)
        {
            SpawnEnemy(enemysPer);
            spawnTimer = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    private void SpawnEnemy(int num)
    {
        // Loop through the number of enemies to spawn
        for (int i = 0; i < num; i++)
        {
            // Check if the current number of enemies is less than the max allowed
            if (currentEnemies < maxEnemies)
            {
                // Spawn the enemy at a random spawn point
                Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius;
                spawnPosition += spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                spawnPosition.y = 0;

                if (Vector3.Distance(spawnPosition, player.position) > minDistance)
                {
                    // Check if the spawn position is valid
                    Collider[] colliders = Physics.OverlapSphere(spawnPosition, 1f);
                    bool isValid = true;
                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag("Enemy"))
                        {
                            isValid = false;
                            break;
                        }
                    }

                    // If the spawn position is valid, instantiate the enemy
                    if (isValid)
                    {
                        // Instantiate the enemy and set its position and rotation
                        Debug.Log("Spawning enemy at: " + spawnPosition);

                        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                        enemy.transform.LookAt(player);
                        currentEnemies++;
                    }
                }
            }
        }
        
    }
}
