using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int currentScore = 0;

    private void OnEnable()
    {
        GameEvents.OnEnemyKilled += AddScore;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyKilled -= AddScore;
    }

    private void AddScore(int amount)
    {
        currentScore += amount;
        scoreText.text = "Score: " + currentScore;
    }
}