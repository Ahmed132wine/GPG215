using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damageToPlayer = 10;

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage(20);
            }

            Destroy(gameObject);
        }
    }
}
