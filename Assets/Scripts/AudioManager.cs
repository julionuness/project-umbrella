using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Fontes de audio")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Musica de fundo")]
    [SerializeField] private AudioClip backgroundMusic;

    [Header("Efeitos sonoros")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip victoryClip;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip buttonClickClip;

    [Header("Passos (variacoes aleatorias)")]
    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField][Range(0f, 0.2f)] private float footstepPitchVariation = 0.08f;

    private int lastFootstepIndex = -1;

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
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayJump() => PlaySfx(jumpClip);
    public void PlayPickup() => PlaySfx(pickupClip);
    public void PlayVictory() => PlaySfx(victoryClip);
    public void PlayGameOver() => PlaySfx(gameOverClip);
    public void PlayHit() => PlaySfx(hitClip);
    public void PlayButtonClick() => PlaySfx(buttonClickClip);

    public void PlayFootstep()
    {
        if (footstepClips == null || footstepClips.Length == 0 || sfxSource == null) return;

        // Sorteia um indice diferente do ultimo tocado, pra nao repetir o mesmo passo 2x seguidas
        int index;
        if (footstepClips.Length == 1)
        {
            index = 0;
        }
        else
        {
            do { index = Random.Range(0, footstepClips.Length); }
            while (index == lastFootstepIndex);
        }
        lastFootstepIndex = index;

        // Pequena variacao de tom pra parecer mais organico
        float originalPitch = sfxSource.pitch;
        sfxSource.pitch = 1f + Random.Range(-footstepPitchVariation, footstepPitchVariation);
        sfxSource.PlayOneShot(footstepClips[index]);
        sfxSource.pitch = originalPitch;
    }

    private void PlaySfx(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }
}