using UnityEngine;
using TMPro;

public enum GameMode { Menu, Playing, Paused, GameOver }
public class GameFlow : MonoBehaviour
{
    public static GameFlow Instance;
    
    [Header("UI Panels")]
    public GameObject menuPanel;
    public GameObject pausePanel;
    public GameObject losePanel;
    
    [Header("UI Text")]
    public TextMeshProUGUI gameplayScoreText;
    public TextMeshProUGUI finalScoreText;
    
    public GameMode currentMode;
    private int score;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        SetMode(GameMode.Menu);
        UpdateScoreDisplay(); 
    }
    public void SetMode(GameMode newMode)
    {
        currentMode = newMode;
        
        Time.timeScale = (newMode == GameMode.Paused || newMode == GameMode.GameOver) ? 0f : 1f;

        if (menuPanel) menuPanel.SetActive(newMode == GameMode.Menu);
        if (pausePanel) pausePanel.SetActive(newMode == GameMode.Paused);
        if (losePanel) losePanel.SetActive(newMode == GameMode.GameOver);

        if (newMode == GameMode.GameOver)
        {
            HandleGameOver();
        }
    }
    
    public void SetModeInt(int modeIndex)
    {
        SetMode((GameMode)modeIndex);
    }

    public void StartGame()
    {
        score = 0;
        UpdateScoreDisplay();
        SetMode(GameMode.Playing);
    }
    
    public void PauseGame() => SetMode(GameMode.Paused);
    public void ResumeGame() => SetMode(GameMode.Playing);

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (gameplayScoreText != null)
            gameplayScoreText.text = "Score: " + score;
    }

    private void HandleGameOver()
    {
        if (finalScoreText != null)
            finalScoreText.text = "Score: " + score;
    }
}
