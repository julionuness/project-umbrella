using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject victoryPanel; // Arraste o Panel de Vitoria aqui
    [SerializeField] private TMP_Text timeText;        // Opcional: texto "Tempo: 00:12"

    void Awake()
    {
        Instance = this;

        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    public void TriggerVictory()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        if (timeText != null)
        {
            float elapsed = Time.timeSinceLevelLoad;
            int minutes = Mathf.FloorToInt(elapsed / 60f);
            int seconds = Mathf.FloorToInt(elapsed % 60f);
            timeText.text = $"Tempo: {minutes:00}:{seconds:00}";
        }

        Time.timeScale = 0f; // Pausa o jogo
    }

    // Chame este metodo no OnClick() de um botao "Jogar novamente" no Canvas
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
