using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float enterSpeed = 3f;          // vertical slide speed
    public float horizontalSpeed = 2f;     // how fast they slide toward player's X
    public float stopOffsetFromTop = 2f;   // how far below top edge we stop
    public float followSmooth = 3f;        // how quickly we lerp toward player.x

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

        float camTop = _cam.transform.position.y + _cam.orthographicSize;
        _targetY = camTop - stopOffsetFromTop; // with size 5 and offset 2 => y = 3

        _state = State.Entering;
    }

    private void Update()
    {
        if (_player == null) return;

        if (_state == State.Entering)
            EnteringMovement();
        else
            AttackingBehaviour();
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

        // Smoothly follow player's X (but slower)
        float targetX = _player.position.x;
        pos.x = Mathf.Lerp(pos.x, targetX, followSmooth * Time.deltaTime);

        // Clamp within camera width
        float halfHeight = _cam.orthographicSize;
        float halfWidth = halfHeight * _cam.aspect;

        pos.x = Mathf.Clamp(
            pos.x,
            _cam.transform.position.x - halfWidth + 0.5f,
            _cam.transform.position.x + halfWidth - 0.5f
        );

        transform.position = pos;

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
    }

}
