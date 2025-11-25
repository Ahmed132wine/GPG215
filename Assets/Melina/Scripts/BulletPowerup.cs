using UnityEngine;

public class BulletPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();
            if (shooting != null)
                shooting.UpgradeBullet();

            Destroy(gameObject);
        }
    }
}