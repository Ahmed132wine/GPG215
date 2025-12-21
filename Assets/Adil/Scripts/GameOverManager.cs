// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
// using TMPro;
//
// public class GameOverManager : MonoBehaviour
// {
//     public static GameOverManager instance;
//
//     [Header("UI Reference")]
//     [SerializeField] private GameObject gameOverPanel;
//     [SerializeField] private TextMeshProUGUI finalScoreText;
//     [SerializeField] private TextMeshProUGUI highScoreText;
//
//     [Header("Buttons")]
//     public Button quitButton;
//
//     private void Awake()
//     {
//         if (instance == null) instance = this;
//     }
//
//     private void Start()
//     {
//         Time.timeScale = 1f;
//         if (gameOverPanel != null) gameOverPanel.SetActive(false);
//     }
//
//     public void TriggerGameOver()
//     {
//         if (gameOverPanel != null)
//         {
//             gameOverPanel.SetActive(true);
//
//             int currentScore = 0;
//
//             if (GameManager.Instance != null)
//             {
//                 currentScore = GameManager.Instance.GetScore();
//
//                 finalScoreText.text = "Score: " + currentScore;
//             }
//
//             int oldHighScore = PlayerPrefs.GetInt("HighScore", 0);
//
//             if (currentScore > oldHighScore)
//             {
//                 PlayerPrefs.SetInt("HighScore", currentScore);
//                 PlayerPrefs.Save();
//                 oldHighScore = currentScore;
//             }
//
//             if (highScoreText != null)
//             {
//                 highScoreText.text = "High Score: " + oldHighScore;
//             }
//         }
//
//         Time.timeScale = 0f;
//     }
//
//     public void RestartGame()
//     {
//         Time.timeScale = 1f;
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//     }
//
//     public void QuitToMenu()
//     {
//         Time.timeScale = 1f;
//         SceneManager.LoadScene(0);
//     }
//     
//   
// }
