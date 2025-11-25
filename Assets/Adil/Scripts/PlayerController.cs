using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float paddingX = 0.5f;

    [Header("Shooting")]
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private Transform firePoint;
    private float nextFireTime = 0f;

    private Camera mainCamera;
    private Vector2 minScreenBounds;
    private Vector2 maxScreenBounds;


    private void Start()
    {
        mainCamera =Camera.main;
        CalculateBounds();
    }
    private void CalculateBounds()
    {
        minScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxScreenBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPos = Input.mousePosition;
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(touchPos);
            float targetX = Mathf.Lerp(transform.position.x, worldPos.x, moveSpeed * Time.deltaTime);
            float clampedX = Mathf.Clamp(targetX, minScreenBounds.x + paddingX, maxScreenBounds.x - paddingX);
            transform.position = new Vector2(clampedX, transform.position.y);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPool.Instance.GetBullet();

        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            bullet.SetActive(true);
        }
    }
}
