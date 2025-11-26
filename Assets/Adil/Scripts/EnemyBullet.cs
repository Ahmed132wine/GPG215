using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float lifeTime = 4f;
    [SerializeField] private int damage = 1;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hook this into your existing player health system
            // Example if your player also implements IDamageable:
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
