using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Sequencia de morte")]
    [Tooltip("Opcional: prefab animado do efeito de concussao. Se vazio, so ha o delay + som.")]
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private float effectLifetime = 1f;
    [SerializeField] private float deathDelay = 0.8f; 

    [Header("Reinicio")]
    [Tooltip("Nome exato da cena da primeira fase. Morrer sempre volta pra ela (regra do GDD).")]
    [SerializeField] private string firstLevelSceneName = "Fase_01";

    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            PlayerMovement movement = player.GetComponent<PlayerMovement>();
            if (movement != null) movement.enabled = false;

            Animator animator = player.GetComponentInChildren<Animator>();
            if (animator != null) animator.SetBool("death", true);

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.simulated = false;
            }

            if (deathEffectPrefab != null)
            {
                GameObject fx = Instantiate(deathEffectPrefab, player.transform.position, Quaternion.identity);
                Destroy(fx, effectLifetime);
            }
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayHit();

        yield return new WaitForSecondsRealtime(deathDelay);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayGameOver();

        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        LoadScene(firstLevelSceneName);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void LoadScene(string sceneName)
    {
        if (SceneTransitionManager.Instance != null)
            SceneTransitionManager.Instance.LoadScene(sceneName);
        else
            SceneManager.LoadScene(sceneName);
    }

    private void LoadScene(int sceneIndex)
    {
        if (SceneTransitionManager.Instance != null)
            SceneTransitionManager.Instance.LoadScene(sceneIndex);
        else
            SceneManager.LoadScene(sceneIndex);
    }
}