using UnityEngine;

public class BulletPowerup : MonoBehaviour
{
    public float fallSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();
            if (shooting != null)
            {
                shooting.UpgradeBullet();
                Debug.Log("Power-up collected");
            }

            Destroy(gameObject);
        }
    }
}