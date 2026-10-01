using UnityEngine;
using UnityEngine.UI;

// Raios de sol girando devagar atras do conteudo da vitoria: faixas finas
// criadas em runtime com o sprite padrao da UI, sem arte externa.
public class SunRaysEffect : MonoBehaviour
{
    [SerializeField] private int rayCount = 10;
    [SerializeField] private float raySize = 1400f;
    [SerializeField] private float rayThickness = 90f;
    [SerializeField] private float rotationSpeed = 2.2f;
    [SerializeField] private Color rayColor = new Color(1f, 0.96f, 0.82f, 0.16f);

    private RectTransform rayGroup;

    void Awake()
    {
        GameObject groupGO = new GameObject("RayGroup", typeof(RectTransform));
        rayGroup = (RectTransform)groupGO.transform;
        rayGroup.SetParent(transform, false);
        rayGroup.anchorMin = new Vector2(0.5f, 0.5f);
        rayGroup.anchorMax = new Vector2(0.5f, 0.5f);
        rayGroup.sizeDelta = Vector2.zero;
        rayGroup.anchoredPosition = Vector2.zero;

        float step = 360f / rayCount;
        for (int i = 0; i < rayCount; i++)
        {
            GameObject go = new GameObject("Ray", typeof(RectTransform), typeof(Image));
            RectTransform rt = (RectTransform)go.transform;
            rt.SetParent(rayGroup, false);
            rt.sizeDelta = new Vector2(rayThickness, raySize);
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = Vector2.zero;
            rt.localRotation = Quaternion.Euler(0f, 0f, step * i);

            Image img = go.GetComponent<Image>();
            img.color = rayColor;
            img.raycastTarget = false;
        }
    }

    void Update()
    {
        rayGroup.Rotate(0f, 0f, rotationSpeed * Time.unscaledDeltaTime);
    }
}
