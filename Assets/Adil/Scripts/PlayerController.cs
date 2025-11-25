using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float paddingX = 0.5f;

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
}
