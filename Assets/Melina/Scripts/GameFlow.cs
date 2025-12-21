using UnityEngine;

public enum GameMode { Menu, Playing, Paused, GameOver }
public class GameFlow : MonoBehaviour
{
    public static GameFlow Instance;
    
    [Header("UI Panels")]
    public GameObject menuPanel;
    public GameObject pausePanel;
    public GameObject losePanel;
    
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
    }
    public void SetMode(GameMode newMode)
    {
        currentMode = newMode;
        
        Time.timeScale = (newMode == GameMode.Paused || newMode == GameMode.GameOver) ? 0f : 1f;

        if (menuPanel) menuPanel.SetActive(newMode == GameMode.Menu);
        if (pausePanel) pausePanel.SetActive(newMode == GameMode.Paused);
        if (losePanel) losePanel.SetActive(newMode == GameMode.GameOver);
    }
    
    public void SetModeInt(int modeIndex)
    {
        SetMode((GameMode)modeIndex);
    }

    public void StartGame()
    {
        SetMode(GameMode.Playing);
    }

    public void PauseGame()
    {
        SetMode(GameMode.Paused);
    }
    
    public void ResumeGame()
    {
        SetMode(GameMode.Playing);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Current Score: " + score);
    }
}
