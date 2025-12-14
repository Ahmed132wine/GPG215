using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Weapon Stats")]
    public GameObject enemyBulletPrefab;
    public Transform firePoint;

    public float minFireRate = 1f;
    public float maxFireRate = 3f;

    private float fireTimer;

    private void Start()
    {
        fireTimer = Random.Range(minFireRate, maxFireRate);
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0)
        {
            Shoot();
            fireTimer = Random.Range(minFireRate, maxFireRate);
        }
    }

    void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            Vector3 spawnPos = (firePoint != null) ? firePoint.position : transform.position;

            Instantiate(enemyBulletPrefab, spawnPos, Quaternion.identity);
        }
    }
}
