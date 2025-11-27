using UnityEngine;

public class EnemySpawner1 : MonoBehaviour
{
    [Header("Settings")]
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    public float xLimit = 2.5f;

    private float nextSpawn = 0f;

    private void Update()
    {
        if (Time.time >= nextSpawn)
        {
            SpawnEnemy();
            nextSpawn = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-xLimit, xLimit);
        Vector3 spawnPos = new Vector3(randomX, 6f, 0);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
