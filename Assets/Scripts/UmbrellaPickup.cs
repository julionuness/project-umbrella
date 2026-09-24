using UnityEngine;

public class UmbrellaPickup : MonoBehaviour
{
    [Header("Animacao de espera (girando)")]
    [SerializeField] private float rotationSpeed = 90f; 

    [Header("Efeito leve de flutuacao (opcional)")]
    [SerializeField] private bool bob = true;
    [SerializeField] private float bobHeight = 0.15f;
    [SerializeField] private float bobSpeed = 2f;

    private Vector3 startPosition;
    private bool collected = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (collected) return;

        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        if (bob)
        {
            float offsetY = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = startPosition + new Vector3(0f, offsetY, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            if (VictoryManager.Instance == null)
            {
                return;
            }

            VictoryManager.Instance.TriggerVictory();

            gameObject.SetActive(false);
        }
    }
}
