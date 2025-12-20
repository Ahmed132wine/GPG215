using UnityEngine;
using System.Collections;
using static NormalWeapon;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]  float moveSpeed = 10f;
    [SerializeField]  float paddingX = 0.5f;

    [Header("Shooting")]
    [SerializeField]  float fireRate = 0.2f;
    [SerializeField]  Transform firePoint;
    float nextFireTime = 0f;

    private IWeaponStrategy currentWeapon;
    private IWeaponStrategy defaultWeapon;
    private IWeaponStrategy overdriveWeapon;

    private Camera mainCamera;
    private Vector2 minScreenBounds;
    private Vector2 maxScreenBounds;


    private void Start()
    {
        mainCamera = Camera.main;
        CalculateBounds();

        defaultWeapon = new NormalWeapon();
        overdriveWeapon = new TripleShotWeapon();

        currentWeapon = defaultWeapon;
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
            if (currentWeapon != null)
            {
                currentWeapon.Fire(firePoint);
            }
            nextFireTime = Time.time + fireRate;
        }
    }

    public void ActivateOverdrive(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(OverdriveRoutine(duration));
    }

    private IEnumerator OverdriveRoutine(float duration)
    {
        currentWeapon = overdriveWeapon;

        yield return new WaitForSeconds(duration);

        currentWeapon = defaultWeapon;
    }
}
