using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour, IDamageable
{

[Header("Stats")]
[SerializeField] private int maxHealth = 3;

[Header("Movement")]
[SerializeField] private float enterSpeed = 2f;      // how fast it slides down into view
[SerializeField] private float horizontalSpeed = 2f; // how fast it slides left/right
[SerializeField] private float stopOffsetFromTop = 0.5f; // how far below top of screen it stops

[Header("Shooting")]
[SerializeField] private GameObject enemyBulletPrefab; // bullet that moves downward
[SerializeField] private Transform firePoint;          // where bullets spawn from
[SerializeField] private float fireRate = 1.2f;        // seconds between shots

private int _currentHealth;
private Transform _player;
private Camera _cam;
private float _targetY;    // world y position where we stop entering
private float _nextFireTime;

private enum State { Entering, Attacking }
private State _state;

private void Start()
{
    _cam = Camera.main;
    GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
    if (playerObj != null)
    {
        _player = playerObj.transform;
    }

    _currentHealth = maxHealth;

    // Calculate the y-position just under the top of the screen
    float camTop = _cam.transform.position.y + _cam.orthographicSize;
    _targetY = camTop - stopOffsetFromTop;

    _state = State.Entering;
}

private void Update()
{
    if (_player == null) return;

    switch (_state)
    {
        case State.Entering:
            HandleEntering();
            break;
        case State.Attacking:
            HandleAttacking();
            break;
    }
}

private void HandleEntering()
{
    Vector3 pos = transform.position;
    pos += Vector3.down * enterSpeed * Time.deltaTime;

    if (pos.y <= _targetY)
    {
        pos.y = _targetY;
        _state = State.Attacking;
    }

    transform.position = pos;
}

private void HandleAttacking()
{
    Vector3 pos = transform.position;

    // Move horizontally toward player, but only along X
    float dx = _player.position.x - pos.x;
    float direction = Mathf.Sign(dx);
    float distance = Mathf.Abs(dx);

    // Small dead zone so it doesn't jitter when very close
    if (distance > 0.05f)
    {
        pos.x += direction * horizontalSpeed * Time.deltaTime;

       
        float halfHeight = _cam.orthographicSize;
        float halfWidth = halfHeight * _cam.aspect;
        pos.x = Mathf.Clamp(
            pos.x,
            _cam.transform.position.x - halfWidth + 0.5f,
            _cam.transform.position.x + halfWidth - 0.5f
        );

        transform.position = pos;
    }

    // Shoot straight down
    if (Time.time >= _nextFireTime)
    {
        Shoot();
        _nextFireTime = Time.time + fireRate;
    }
}

private void Shoot()
{
    if (enemyBulletPrefab == null || firePoint == null) return;
    Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);
    // Enemy bullet script will handle moving down.
}

// IDamageable
public void TakeDamage(int amount)
{
    _currentHealth -= amount;
    if (_currentHealth <= 0)
    {
        Die();
    }
}

private void Die()
{
   
    if (EnemySpawner.Instance != null)
    {
        EnemySpawner.Instance.NotifyEnemyDestroyed(this);
    }

    Destroy(gameObject);
}
}
