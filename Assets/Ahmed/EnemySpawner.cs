using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("Spawning")]
    public GameObject enemyPrefab;
    public int maxEnemies = 2;
    public float spawnYOffset = 3f;
    public float minHorizontalSeparation = 1.5f;

    private int activeEnemies = 0;
    private Camera cam;
    private float lastSpawnX = 999f;

    // -1, +1, -1, +1...
    private int nextLaneIndex = -1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = Camera.main;

        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        if (enemyPrefab == null || cam == null) return;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        float x;
        int attempts = 0;

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

        float y = cam.transform.position.y + halfHeight + spawnYOffset;

        GameObject enemyObj = Instantiate(enemyPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        activeEnemies++;

        // Assign lane (-1 or +1) to this enemy
        EnemyController ctrl = enemyObj.GetComponent<EnemyController>();
        if (ctrl != null)
        {
            ctrl.SetLane(nextLaneIndex);
            nextLaneIndex *= -1; // flip for next spawn
        }
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
