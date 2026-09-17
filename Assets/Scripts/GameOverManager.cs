using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel; // Arraste o Panel de Game Over aqui

    void Awake()
    {
        // Singleton simples (sem DontDestroyOnLoad, pois cada cena de jogo tem o seu)
        Instance = this;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void TriggerGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f; // Pausa o jogo
    }

    // Chame este metodo no OnClick() do botao "Reiniciar" no Canvas
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Volta o tempo ao normal antes de recarregar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
