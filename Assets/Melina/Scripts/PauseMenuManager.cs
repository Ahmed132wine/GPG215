using UnityEngine;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;     
    public TextMeshProUGUI pauseText; 
    public GameObject pauseButton;    

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        pauseText.gameObject.SetActive(false);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            pauseText.gameObject.SetActive(true);
            pauseText.ForceMeshUpdate();
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            pauseText.gameObject.SetActive(false);
        }
    }
}