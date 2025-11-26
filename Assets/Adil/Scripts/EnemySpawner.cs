using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private int maxEnemiesOnScreen = 2;

    private Camera _cam;
    private int _currentEnemies;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _cam = Camera.main;

        // Start with a full "wave"
        for (int i = 0; i < maxEnemiesOnScreen; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        float halfHeight = _cam.orthographicSize;
        float halfWidth = halfHeight * _cam.aspect;

        // Random X within screen width
        float x = Random.Range(
            _cam.transform.position.x - halfWidth + 0.5f,
            _cam.transform.position.x + halfWidth - 0.5f
        );

        // Spawn just above the top of the screen
        float y = _cam.transform.position.y + halfHeight + 1f;

        Instantiate(enemyPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        _currentEnemies++;
    }

    public void NotifyEnemyDestroyed(EnemyController enemy)
    {
        _currentEnemies--;

        // As soon as an enemy is destroyed, spawn a replacement
        if (_currentEnemies < maxEnemiesOnScreen)
        {
            SpawnEnemy();
        }
    }
}
