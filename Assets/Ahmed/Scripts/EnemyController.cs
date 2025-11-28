using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float enterSpeed = 3f;          // vertical slide speed
    public float horizontalSpeed = 2f;     // follow speed on X
    public float stopOffsetFromTop = 2f;   // how far below top edge we stop
    public float laneOffset = 1.5f;        // horizontal offset from player for each lane

    [Header("Shooting")]
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.2f;

    private Camera _cam;
    private Transform _player;
    private float _targetY;
    private float _nextFireTime;

    // -1 = left, +1 = right
    private int _laneIndex = 0;

    private enum State { Entering, Attacking }
    private State _state;

    private void Start()
    {
        _cam = Camera.main;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            _player = playerObj.transform;

        float camTop = _cam.transform.position.y + _cam.orthographicSize;
        _targetY = camTop - stopOffsetFromTop;

        _state = State.Entering;
    }

    // Called by the spawner right after instantiate
    public void SetLane(int laneIndex)
    {
        _laneIndex = Mathf.Clamp(laneIndex, -1, 1);
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

        // target lane position relative to player
        float targetX = _player.position.x + _laneIndex * laneOffset;
        float dx = targetX - pos.x;
        float dir = Mathf.Sign(dx);

        if (Mathf.Abs(dx) > 0.05f)
        {
            pos.x += dir * horizontalSpeed * Time.deltaTime;

            float halfHeight = _cam.orthographicSize;
            float halfWidth = halfHeight * _cam.aspect;

            pos.x = Mathf.Clamp(
                pos.x,
                _cam.transform.position.x - halfWidth + 0.5f,
                _cam.transform.position.x + halfWidth - 0.5f
            );
        }

        transform.position = pos;

        // shooting
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
