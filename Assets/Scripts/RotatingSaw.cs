using UnityEngine;

// Coloque num GameObject com Collider2D (Is Trigger = true) representando a serra/obstaculo.
[RequireComponent(typeof(Rigidbody2D))]
public class RotatingSaw : MonoBehaviour
{
    [Header("Rotacao visual")]
    [SerializeField] private float rotationSpeed = 200f;

    [Header("Movimento de ida e volta")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 target;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Comeca indo em direcao ao ponto B
        if (pointA != null) rb.position = pointA.position;
        target = pointB != null ? (Vector2)pointB.position : rb.position;
    }

    void Update()
    {
        // Girar e so visual, nao interfere no movimento (que usa o Rigidbody)
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (pointA == null || pointB == null) return;

        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        // Ao chegar bem perto do alvo atual, inverte pro outro ponto -- sem pausa, sem teleporte
        if (Vector2.Distance(rb.position, target) < 0.05f)
        {
            target = (target == (Vector2)pointA.position) ? (Vector2)pointB.position : (Vector2)pointA.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Por enquanto usa o mesmo Game Over do limbo.
            // Depois, se você quiser vidas/dano em vez de morte instantânea,
            // é só trocar essa linha por uma chamada tipo player.TakeDamage(1).
            GameOverManager.Instance.TriggerGameOver();
        }
    }
}