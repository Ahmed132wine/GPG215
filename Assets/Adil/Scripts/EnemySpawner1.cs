using UnityEngine;

public class EnemySpawner1 : MonoBehaviour
{
    public EnemyFactory factory;

    [Header("Settings")] public float initialSpawnRate = 2f;
    public float minimumSpawnRate = 0.5f;
    public float difficultyRamp = 0.05f;

    private float currentSpawnRate;
    private float nextSpawn = 0f;
    private Camera mainCamera;
    private float enemyWidth = 0.5f;

    private void Start()
    {
        currentSpawnRate = initialSpawnRate;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (GameFlow.Instance.currentMode != GameMode.Playing) return;

        if (Time.time >= nextSpawn)
        {
            SpawnEnemy();

            currentSpawnRate -= difficultyRamp;
            if (currentSpawnRate < minimumSpawnRate)
                currentSpawnRate = minimumSpawnRate;

            nextSpawn = Time.time + currentSpawnRate;
        }

    }

    private void SpawnEnemy()
    {
        float screenHalfWidthInWorld = mainCamera.aspect * mainCamera.orthographicSize;
        float xLimit = screenHalfWidthInWorld - enemyWidth;
        float randomX = Random.Range(-xLimit, xLimit);
        float spawnY = mainCamera.orthographicSize + 2f;

        Vector3 spawnPos = new Vector3(randomX, spawnY, 0);

        string type = (Random.value > 0.7f) ? "Fast" : "Simple";

        GameObject enemy = factory.MakeEnemy(type);

        if (enemy != null)
        {
            enemy.transform.position = spawnPos;
            enemy.transform.rotation = Quaternion.identity;
        }
    }
}

