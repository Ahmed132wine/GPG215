using UnityEngine;

public class EnemySpawner1 : MonoBehaviour
{
    [Header("Settings")]
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    public float xLimit = 2.5f;

    [Header("Difficulty")]
    public float initialSpawnRate = 2f;
    public float minimumSpawnRate = 0.5f;
    public float difficultyRamp = 0.05f;

    private float currentSpawnRate;
    private float nextSpawn = 0f;

    private void Start()
    {
        currentSpawnRate = initialSpawnRate;
    }

    private void Update()
    {
        if (Time.time >= nextSpawn)
        {
            SpawnEnemy();
            currentSpawnRate -= difficultyRamp;

            if (currentSpawnRate < minimumSpawnRate)
                currentSpawnRate = minimumSpawnRate;

            nextSpawn = Time.time + currentSpawnRate;
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-xLimit, xLimit);
        Vector3 spawnPos = new Vector3(randomX, 6f, 0);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
