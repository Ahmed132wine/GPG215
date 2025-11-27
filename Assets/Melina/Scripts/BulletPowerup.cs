using UnityEngine;

public class BulletPowerup : MonoBehaviour
{
    public float fallSpeed = 3f;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();

            if (shooting != null)
            {
                shooting.ActivateOverdrive();
            }
            
            Destroy(gameObject);
        }
    }
}