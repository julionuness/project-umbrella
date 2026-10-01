using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TMP_Text timeText;

    void Awake()
    {
        Instance = this;

        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    public void TriggerVictory()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("[VictoryManager] victoryPanel esta NULL nesta instancia!");
        }

        if (timeText != null)
        {
            float elapsed = GameTimer.Instance != null ? GameTimer.Instance.ElapsedSeconds : Time.timeSinceLevelLoad;
            int minutes = Mathf.FloorToInt(elapsed / 60f);
            int seconds = Mathf.FloorToInt(elapsed % 60f);
            timeText.text = $"Tempo: {minutes:00}:{seconds:00}";
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayVictory();

        Time.timeScale = 0f;
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int targetIndex = (nextIndex < SceneManager.sceneCountInBuildSettings) ? nextIndex : 0;

        if (SceneTransitionManager.Instance != null)
            SceneTransitionManager.Instance.LoadScene(targetIndex);
        else
            SceneManager.LoadScene(targetIndex);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if (SceneTransitionManager.Instance != null)
            SceneTransitionManager.Instance.LoadScene(currentIndex);
        else
            SceneManager.LoadScene(currentIndex);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;

        if (SceneTransitionManager.Instance != null)
            SceneTransitionManager.Instance.LoadScene(0);
        else
            SceneManager.LoadScene(0);
    }
}