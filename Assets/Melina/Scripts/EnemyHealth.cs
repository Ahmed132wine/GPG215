using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 20f;
    private float currentHealth;

    [Header("UI")]
    public Canvas healthCanvas;
    public Image healthFill;
    private float hideTimer;

    [Header("Power-Up")]
    public GameObject bulletPowerupPrefab;

    [Header("Score")]
    public int scoreValue = 100;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthCanvas != null) healthCanvas.enabled = false;
    }

    void Update()
    {
        hideTimer -= Time.deltaTime;
        if (hideTimer <= 0 && healthCanvas != null)
            healthCanvas.enabled = false;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        if (healthFill != null)
            healthFill.fillAmount = currentHealth / maxHealth;

        if (healthCanvas != null)
        {
            healthCanvas.enabled = true;
            hideTimer = 1.5f;
        }

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (bulletPowerupPrefab != null)
            Instantiate(bulletPowerupPrefab, transform.position, Quaternion.identity);

        GameManager.Instance?.AddScore(scoreValue);
        EnemySpawner.Instance?.NotifyEnemyDestroyed(gameObject);
        Destroy(gameObject);
    }
}