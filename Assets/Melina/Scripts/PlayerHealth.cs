using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        
        AudioManager.Instance?.PlayPlayerDamage();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "HP: " + currentHealth;
    }

    void Die()
    {
        if (GameOverManager.instance != null)
        {
            GameOverManager.instance.TriggerGameOver();
        }

        Debug.Log("Player Died!!!");
    }
}
