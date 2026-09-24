using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Coloque este script num GameObject dentro de um Canvas com uma Image preta cobrindo a tela toda.
// Esse GameObject deve existir SO na primeira cena que carrega (ex: Menu), porque ele proprio
// se torna persistente (DontDestroyOnLoad) e viaja com o jogador por todas as cenas seguintes.
public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [Header("Fade")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Fade-in suave assim que o jogo abre (comeca preto, revela a cena)
        StartCoroutine(Fade(1f, 0f));
    }

    // Chame isso ao inves de SceneManager.LoadScene(nomeDaCena)
    public void LoadScene(string sceneName)
    {
        StartCoroutine(TransitionRoutine(sceneName, -1));
    }

    // Chame isso ao inves de SceneManager.LoadScene(indice)
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(TransitionRoutine(null, sceneIndex));
    }

    private IEnumerator TransitionRoutine(string sceneName, int sceneIndex)
    {
        yield return StartCoroutine(Fade(0f, 1f)); // escurece

        if (sceneName != null)
            SceneManager.LoadScene(sceneName);
        else
            SceneManager.LoadScene(sceneIndex);

        yield return StartCoroutine(Fade(1f, 0f)); // revela a cena nova
    }

    private IEnumerator Fade(float from, float to)
    {
        if (fadeImage == null) yield break;

        float t = 0f;
        Color c = fadeImage.color;

        // unscaledDeltaTime: funciona mesmo se Time.timeScale estiver em 0 (pausado)
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(from, to, t / fadeDuration);
            fadeImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(c.r, c.g, c.b, to);
    }
}