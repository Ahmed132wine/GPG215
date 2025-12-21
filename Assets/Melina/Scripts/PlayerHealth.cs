using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 100;
    private int currentHealth;
    
    [Header("UI")]
    public TextMeshProUGUI healthText;
    
    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        if (GameFlow.Instance.currentMode != GameMode.Playing) return;
        currentHealth -= amount;
        
        if (CameraShake.Instance != null) CameraShake.Instance.TriggerShake(0.2f);
        AudioManager.Instance?.PlayPlayerDamage();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameFlow.Instance.SetMode(GameMode.GameOver);
        }
        
        UpdateUI();
    }
    
    void UpdateUI()
    {
        if (healthText != null)
            healthText.text = "HP: " + currentHealth;
    }
}
