using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 4f;
    public int damage = 10;

    private void OnEnable()
    {
        Invoke(nameof(Hide), lifeTime);
    }

    private void Update()
    {
        // Move straight down in world space
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth hp = other.GetComponent<PlayerHealth>();
            if (hp != null)
                hp.TakeDamage(damage);  // uses your existing PlayerHealth script :contentReference[oaicite:1]{index=1}

            Hide();
        }
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
