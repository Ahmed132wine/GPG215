using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;       
    public float turnSpeed = 200f;

    [Header("Controls")]
    public KeyCode turnLeftKey = KeyCode.A;
    public KeyCode turnRightKey = KeyCode.D;

    private Rigidbody2D rb;
    private bool canMove = true;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = false;

        //  transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.linearVelocity = transform.up * moveSpeed;
    }


    private void FixedUpdate()
    {
        if (!canMove) return;

        float rotation = rb.rotation;

        if (Input.GetKey(turnLeftKey))
        {
            rotation += turnSpeed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(turnRightKey))
        {
            rotation -= turnSpeed * Time.fixedDeltaTime;
        }

        rb.MoveRotation(rotation);
        rb.linearVelocity = transform.up * moveSpeed;
    }

    private void OnCollisionEnter(Collision2D collision)
    {
        Debug.Log(gameObject.name + " hit " + collision.collider.name);

        canMove = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.Sleep();
    }

    private void OnDisable()
    {
        if (rb != null)
        {
            rb.WakeUp();
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.WakeUp();
            canMove = true;
            transform.rotation = Quaternion.identity;
            rb.rotation = 0;
        }
    }
}

