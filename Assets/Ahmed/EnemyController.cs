using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float enterSpeed = 2f;           // how fast enemy slides down into view
    public float horizontalSpeed = 2f;      // left/right follow speed
    public float stopOffsetFromTop = 0.5f;  // how far below top of screen enemy stops

    [Header("Shooting")]
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.2f;

    private Camera _cam;
    private Transform _player;
    private float _targetY;
    private float _nextFireTime;

    private enum State { Entering, Attacking }
    private State _state;

    private void Start()
    {
        _cam = Camera.main;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            _player = playerObj.transform;

        // Calculate Y where enemy stops
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
                EnteringMovement();
                break;

            case State.Attacking:
                AttackingBehaviour();
                break;
        }
    }

    private void EnteringMovement()
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

    private void AttackingBehaviour()
    {
        Vector3 pos = transform.position;

        // Horizontal follow only
        float dx = _player.position.x - pos.x;
        float direction = Mathf.Sign(dx);

        if (Mathf.Abs(dx) > 0.05f)
        {
            pos.x += direction * horizontalSpeed * Time.deltaTime;

            // Clamp within camera horizontal bounds
            float halfHeight = _cam.orthographicSize;
            float halfWidth = halfHeight * _cam.aspect;
            pos.x = Mathf.Clamp(
                pos.x,
                _cam.transform.position.x - halfWidth + 0.5f,
                _cam.transform.position.x + halfWidth - 0.5f
            );
        }

        transform.position = pos;

        // Shoot downward
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
    }

}
