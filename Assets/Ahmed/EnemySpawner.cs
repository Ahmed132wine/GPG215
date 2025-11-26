using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("Spawning")]
    public GameObject enemyPrefab;
    public int maxEnemies = 2;
    public float spawnYOffset = 3f;             // above top of screen
    public float minHorizontalSeparation = 1.5f;

    private int activeEnemies = 0;
    private Camera cam;
    private float lastSpawnX = 999f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = Camera.main;

        // Spawn initial enemies
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        if (enemyPrefab == null || cam == null) return;

        float halfHeight = cam.orthographicSize;      // 5
        float halfWidth = halfHeight * cam.aspect;   // depends on 9:16

        float x;
        int attempts = 0;

        // try to avoid spawning too close to previous X
        do
        {
            x = Random.Range(
                cam.transform.position.x - halfWidth + 0.5f,
                cam.transform.position.x + halfWidth - 0.5f
            );
            attempts++;
        }
        while (Mathf.Abs(x - lastSpawnX) < minHorizontalSeparation && attempts < 10);

        lastSpawnX = x;

        float y = cam.transform.position.y + halfHeight + spawnYOffset; // 5 + 3 = 8

        Instantiate(enemyPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        activeEnemies++;
    }

    public void NotifyEnemyDestroyed()
    {
        activeEnemies--;

        if (activeEnemies < maxEnemies)
        {
            SpawnEnemy();
        }
    }
}
