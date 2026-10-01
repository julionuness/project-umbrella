using UnityEngine;
using UnityEngine.UI;

// Confete caindo, girando e balancando em loop dentro deste RectTransform
// (precisa ocupar a tela toda). Cada pedaco e criado em runtime com o
// sprite padrao da UI, sem arte externa. Usa Time.unscaledDeltaTime porque
// a tela de vitoria roda com Time.timeScale = 0.
public class ConfettiEffect : MonoBehaviour
{
    [SerializeField] private int pieceCount = 36;
    [SerializeField] private float minSpeed = 160f;
    [SerializeField] private float maxSpeed = 320f;
    [SerializeField] private float minSpin = 60f;
    [SerializeField] private float maxSpin = 220f;
    [SerializeField] private Vector2 pieceSize = new Vector2(10f, 14f);
    [SerializeField]
    private Color[] palette = new Color[]
    {
        new Color(0.91f, 0.64f, 0.24f),
        new Color(0.70f, 0.31f, 0.25f),
        new Color(0.97f, 0.95f, 0.89f),
        new Color(0.44f, 0.57f, 0.31f)
    };

    private RectTransform area;
    private RectTransform[] pieces;
    private float[] fallSpeed;
    private float[] spinSpeed;
    private float[] swaySeed;

    void OnEnable()
    {
        area = (RectTransform)transform;

        if (pieces == null)
            BuildPieces();

        for (int i = 0; i < pieces.Length; i++)
            ResetPiece(i, true);
    }

    private void BuildPieces()
    {
        pieces = new RectTransform[pieceCount];
        fallSpeed = new float[pieceCount];
        spinSpeed = new float[pieceCount];
        swaySeed = new float[pieceCount];

        for (int i = 0; i < pieceCount; i++)
        {
            GameObject go = new GameObject("Confetti", typeof(RectTransform), typeof(Image));
            RectTransform rt = (RectTransform)go.transform;
            rt.SetParent(area, false);
            rt.sizeDelta = pieceSize;
            rt.anchorMin = rt.anchorMax = new Vector2(0f, 1f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            Image img = go.GetComponent<Image>();
            img.color = palette[i % palette.Length];
            img.raycastTarget = false;

            pieces[i] = rt;
        }
    }

    private void ResetPiece(int i, bool randomizeHeight)
    {
        float areaWidth = area.rect.width;
        float areaHeight = area.rect.height;
        float x = Random.Range(0f, areaWidth);
        float startY = randomizeHeight ? Random.Range(-areaHeight, areaHeight * 0.3f) : Random.Range(0f, areaHeight * 0.3f);

        pieces[i].anchoredPosition = new Vector2(x, startY);
        pieces[i].localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        fallSpeed[i] = Random.Range(minSpeed, maxSpeed);
        spinSpeed[i] = Random.Range(minSpin, maxSpin) * (Random.value < 0.5f ? -1f : 1f);
        swaySeed[i] = Random.Range(0f, 100f);
    }

    void Update()
    {
        if (pieces == null) return;

        float areaHeight = area.rect.height;
        float areaWidth = area.rect.width;

        for (int i = 0; i < pieces.Length; i++)
        {
            RectTransform rt = pieces[i];
            Vector2 pos = rt.anchoredPosition;
            pos.y -= fallSpeed[i] * Time.unscaledDeltaTime;
            pos.x += Mathf.Sin((Time.unscaledTime + swaySeed[i]) * 1.3f) * 18f * Time.unscaledDeltaTime;

            rt.anchoredPosition = pos;
            rt.Rotate(0f, 0f, spinSpeed[i] * Time.unscaledDeltaTime);

            if (pos.y < -areaHeight - pieceSize.y)
            {
                pos.y = Random.Range(0f, areaHeight * 0.3f);
                pos.x = Random.Range(0f, areaWidth);
                rt.anchoredPosition = pos;
                fallSpeed[i] = Random.Range(minSpeed, maxSpeed);
            }
        }
    }
}
