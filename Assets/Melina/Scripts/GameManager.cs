using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score UI")]
    public TextMeshProUGUI scoreText;

    public int score = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Application.targetFrameRate = 60;
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public int GetScore()
    {
        return score;
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void HitStop(float duration)
    {
        if (Time.timeScale > 0)
            StartCoroutine(DoHitStop(duration));
    }

    IEnumerator DoHitStop(float duration)
    {
        float originalTimeScale = Time.timeScale;

        Time.timeScale = 0.0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = originalTimeScale;
    }
}