using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthFill;
    public Canvas healthCanvas;
    private float hideTimer;

    void Awake()
    {
        currentHealth = maxHealth;
        if (healthCanvas != null)
            healthCanvas.enabled = false;
    }

    void Update()
    {
        if (healthCanvas == null) return;

        if (hideTimer > 0)
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0)
                healthCanvas.enabled = false;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

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
        Destroy(gameObject);
    }
}