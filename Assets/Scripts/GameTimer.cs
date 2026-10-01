using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Cronometro geral da partida: comeca do zero quando o jogador aperta "Jogar"
// no menu e roda ate a vitoria final, sobrevivendo as trocas de cena entre as
// fases (DontDestroyOnLoad). Usa Time.deltaTime (nao unscaled) de proposito:
// assim ele pausa sozinho sempre que o jogo pausa (Game Over / Vitoria setam
// Time.timeScale = 0), em vez de continuar contando enquanto o painel esta aberto.
public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    [Header("HUD")]
    [SerializeField] private GameObject hudRoot;
    [SerializeField] private TMP_Text hudText;

    public float ElapsedSeconds { get; private set; }

    private bool inMenu;

    // Garante que o GameTimer exista desde a primeira cena que rodar, mesmo que
    // o Editor abra direto numa fase (sem passar pelo Menu). O prefab fica em
    // Assets/Resources/GameTimer.prefab.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        if (Instance != null) return;

        GameObject prefab = Resources.Load<GameObject>("GameTimer");
        if (prefab != null)
            Instantiate(prefab);
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        inMenu = SceneManager.GetActiveScene().buildIndex == 0;
        if (hudRoot != null)
            hudRoot.SetActive(!inMenu);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        inMenu = scene.buildIndex == 0;

        if (hudRoot != null)
            hudRoot.SetActive(!inMenu);
    }

    // Chamado ao apertar "Jogar" no menu: zera o relogio para uma corrida nova.
    public void StartNewRun()
    {
        ElapsedSeconds = 0f;
        UpdateHudText();
    }

    void Update()
    {
        if (inMenu) return;

        ElapsedSeconds += Time.deltaTime;
        UpdateHudText();
    }

    private void UpdateHudText()
    {
        if (hudText == null) return;

        int minutes = Mathf.FloorToInt(ElapsedSeconds / 60f);
        int seconds = Mathf.FloorToInt(ElapsedSeconds % 60f);
        hudText.text = $"{minutes:00}:{seconds:00}";
    }
}
