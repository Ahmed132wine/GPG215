using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5f;
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    [Header("References")]
    public GameObject wallPrefab;
    public Transform wallHolder;

    private Collider2D myCollider;
    private Vector2 direction;
    private GameObject currentWall;
    private Vector2 lastWallEndPosition;


    private void Start()
    {
        myCollider = GetComponent<Collider2D>();
        direction = Vector2.up;
        SpawnWall();
    }

    private void Update()
    {
        Move();
        CheckInput();
    }

    void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(upKey) && direction != Vector2.down)
        {
            Turn(Vector2.up);
        }
        else if (Input.GetKeyDown(downKey) && direction != Vector2.up)
        {
            Turn(Vector2.down);
        }
        else if (Input.GetKeyDown(leftKey) && direction != Vector2.right)
        {
            Turn(Vector2.left);
        }
        else if (Input.GetKeyDown(rightKey) && direction != Vector2.left)
        {
            Turn(Vector2.right);
        }
    }

    void SpawnWall()
    {
        lastWallEndPosition = transform.position;
        currentWall = Instantiate(wallPrefab, transform.position, Quaternion.identity);
        if (wallHolder != null) currentWall.transform.SetParent(wallHolder);
    }

    void UpdateWall()
    {
        FitWallToGap();
    }

    void Turn( Vector2 newDirection)
    {
        if (direction == newDirection) return;

        FitWallToGap();

        direction = newDirection;

        SpawnWall();
    }

    void FitWallToGap()
    {
        if (currentWall == null) return;
        Vector2 currentPos = transform.position;
        float distance = Vector2.Distance(lastWallEndPosition, currentPos);

        currentWall.transform.position = lastWallEndPosition + (direction * distance * 0.5f);

        if (direction == Vector2.up || direction == Vector2.down)
        {
            currentWall.transform.localScale = new Vector3(0.2f, distance, 1f);
        }
        else
        {
            currentWall.transform.localScale = new Vector3(distance, 0.2f, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform != currentWall.transform)
        {
            FindObjectOfType<GameManager>().GameOver();
            Destroy(gameObject);
        }
    }
}
