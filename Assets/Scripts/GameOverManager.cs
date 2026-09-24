using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void TriggerGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayGameOver();

        Time.timeScale = 0f; 
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