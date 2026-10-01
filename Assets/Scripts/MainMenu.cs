using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Tooltip("Nome exato da cena da primeira fase (ex: Fase_01). Precisa estar no Build Settings.")]
    [SerializeField] private string firstLevelSceneName = "Fase_01";

    public void PlayGame()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();

        if (GameTimer.Instance != null)
            GameTimer.Instance.StartNewRun();

        if (SceneTransitionManager.Instance != null)
            SceneTransitionManager.Instance.LoadScene(firstLevelSceneName);
        else
            SceneManager.LoadScene(firstLevelSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}