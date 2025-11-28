using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;

    [Header("Player, Enemy & Game UI")]
    public GameObject player;
    public GameObject playerUI;
    public GameObject enemy;
    public GameObject enemyUI;
    public GameObject scrollingBackground;

    [Header("UI Elements")]
    public TextMeshProUGUI gameTitleText;

    [Header("Buttons")]
    public Button startButton;
    public Button backButton;
    public Button quitButton;

    void Awake()
    {
        if (startButton != null) startButton.onClick.AddListener(StartGame);
        if (quitButton != null) quitButton.onClick.AddListener(QuitGame);
    }

    void Start()
    {
        if (gameTitleText != null)
            gameTitleText.text = gameTitleText.text;

        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);

        if (player != null) player.SetActive(false);
        if (playerUI != null) playerUI.SetActive(false);
        if (enemy != null) enemy.SetActive(false);
        if (enemyUI != null) enemyUI.SetActive(false);

        if (scrollingBackground != null) scrollingBackground.SetActive(false);

        if (backButton != null)
            backButton.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);

        if (player != null) player.SetActive(true);
        if (playerUI != null) playerUI.SetActive(true);
        if (enemy != null) enemy.SetActive(true);
        if (enemyUI != null) enemyUI.SetActive(true);

        if (scrollingBackground != null) scrollingBackground.SetActive(true);

        if (backButton != null)
            backButton.gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        ShowMainMenu();
    }

    public void QuitGame()
    {
        Debug.Log("Quit pressed");
        Application.Quit();
    }
}
