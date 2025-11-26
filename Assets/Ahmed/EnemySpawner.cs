using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public GameObject enemyPrefab;
    public int maxEnemies = 2;

    private int activeEnemies = 0;
    private Camera cam;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = Camera.main;

        for (int i = 0; i < maxEnemies; i++)
            SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        float x = Random.Range(
            cam.transform.position.x - halfWidth + 0.5f,
            cam.transform.position.x + halfWidth - 0.5f
        );

        float y = cam.transform.position.y + halfHeight + 1f;

        Instantiate(enemyPrefab, new Vector3(x, y, 0f), Quaternion.identity);

        activeEnemies++;
    }

    public void NotifyEnemyDestroyed(GameObject enemy)
    {
        activeEnemies--;

        if (activeEnemies < maxEnemies)
        {
            SpawnEnemy();
        }
    }
}
