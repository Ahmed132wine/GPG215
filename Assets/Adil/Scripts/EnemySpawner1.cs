using UnityEngine;

public class EnemySpawner1 : MonoBehaviour
{
    [Header("Settings")]
    public GameObject enemyPrefab;
    public float initialSpawnRate = 2f;
    public float minimumSpawnRate = 0.5f;
    public float difficultyRamp = 0.05f;

    private float currentSpawnRate;
    private float nextSpawn = 0f;
    private Camera mainCamera;
    private float enemyWidth;

    private void Start()
    {
        currentSpawnRate = initialSpawnRate;
        mainCamera = Camera.main;

        if (enemyPrefab != null)
        {
            SpriteRenderer sr = enemyPrefab.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                enemyWidth = sr.bounds.extents.x;
            }
        }
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
        float screenHalfWidthInWorld = mainCamera.aspect * mainCamera.orthographicSize;

        float xLimit = screenHalfWidthInWorld - enemyWidth;

        float randomX = Random.Range(-xLimit, xLimit);

        float spawnY = mainCamera.orthographicSize + 2f;

        Vector3 spawnPos = new Vector3(randomX, spawnY, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

    }
}
