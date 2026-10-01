using UnityEngine;
using UnityEngine.UI;

// Chuva caindo em loop dentro deste RectTransform (precisa ocupar a tela toda).
// Cada gota e criada em runtime com o sprite padrao da UI, sem arte externa.
// Usa Time.unscaledDeltaTime porque a tela de derrota roda com Time.timeScale = 0.
public class RainEffect : MonoBehaviour
{
    [SerializeField] private int dropCount = 40;
    [SerializeField] private float minSpeed = 900f;
    [SerializeField] private float maxSpeed = 1400f;
    [SerializeField] private float dropWidth = 2f;
    [SerializeField] private float dropHeight = 26f;
    [SerializeField] private Color dropColor = new Color(0.78f, 0.85f, 0.95f, 0.5f);

    private RectTransform area;
    private RectTransform[] drops;
    private float[] speeds;

    void OnEnable()
    {
        area = (RectTransform)transform;

        if (drops == null)
            BuildDrops();

        for (int i = 0; i < drops.Length; i++)
            ResetDrop(i, true);
    }

    private void BuildDrops()
    {
        drops = new RectTransform[dropCount];
        speeds = new float[dropCount];

        for (int i = 0; i < dropCount; i++)
        {
            GameObject go = new GameObject("Drop", typeof(RectTransform), typeof(Image));
            RectTransform rt = (RectTransform)go.transform;
            rt.SetParent(area, false);
            rt.sizeDelta = new Vector2(dropWidth, dropHeight);
            rt.anchorMin = rt.anchorMax = new Vector2(0f, 1f);
            rt.pivot = new Vector2(0.5f, 1f);

            Image img = go.GetComponent<Image>();
            img.color = dropColor;
            img.raycastTarget = false;

            drops[i] = rt;
        }
    }

    private void ResetDrop(int i, bool randomizeHeight)
    {
        float areaWidth = area.rect.width;
        float areaHeight = area.rect.height;
        float x = Random.Range(0f, areaWidth);
        float startY = randomizeHeight ? Random.Range(-areaHeight, 0f) : Random.Range(0f, areaHeight * 0.3f);

        drops[i].anchoredPosition = new Vector2(x, startY);
        speeds[i] = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        if (drops == null) return;

        float areaHeight = area.rect.height;
        float areaWidth = area.rect.width;

        for (int i = 0; i < drops.Length; i++)
        {
            RectTransform rt = drops[i];
            Vector2 pos = rt.anchoredPosition;
            pos.y -= speeds[i] * Time.unscaledDeltaTime;

            if (pos.y < -areaHeight - dropHeight)
            {
                pos.y = Random.Range(0f, areaHeight * 0.3f);
                pos.x = Random.Range(0f, areaWidth);
                speeds[i] = Random.Range(minSpeed, maxSpeed);
            }

            rt.anchoredPosition = pos;
        }
    }
}
