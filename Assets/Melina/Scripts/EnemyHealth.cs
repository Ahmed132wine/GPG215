using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 20f;
    private float currentHealth;

    [Header("Visual Effects")]
    public GameObject explosionPrefab;
    public bool shakeCameraOnDeath = true;

    [Header("UI")]
    public Canvas healthCanvas;
    public Image healthFill;
    private float hideTimer;

    [Header("Power-Up")]
    public GameObject bulletPowerupPrefab;

    [Header("Score")]
    public int scoreValue = 100;

    [Range(0, 100)]
    public float dropChance = 20f;

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthCanvas != null) healthCanvas.enabled = false;
    }

    private void Update()
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
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        if (shakeCameraOnDeath && CameraShake.Instance != null)
            CameraShake.Instance.TriggerShake(0.05f);

        if (Random.Range(0f, 100f) <= dropChance && bulletPowerupPrefab != null)
            Instantiate(bulletPowerupPrefab, transform.position, Quaternion.identity);

        GameEvents.OnEnemyKilled?.Invoke(scoreValue);

        Destroy(gameObject);
    }
}