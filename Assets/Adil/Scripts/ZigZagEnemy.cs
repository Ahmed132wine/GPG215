using UnityEngine;

public class ZigZagEnemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float fallSpeed = 3f;
    public float waveFrequency = 2f;
    public float waveWidth = 3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        startPos += Vector3.down * fallSpeed * Time.deltaTime;

        float offset = Mathf.Sin(Time.time * waveFrequency) * waveWidth;

        transform.position = startPos + new Vector3(offset, 0, 0);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var hp = other.GetComponent<PlayerHealth>();
            if (hp != null) hp.TakeDamage(20);

            Destroy(gameObject);
        }
    }
}


