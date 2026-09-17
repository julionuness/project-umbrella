using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    [Header("Pontos de movimento")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Configuracao")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool startAtPointA = true;

    private Rigidbody2D rb;
    private Vector2 target;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Plataforma precisa ser Kinematic: se movimenta por script, mas o player
        // consegue ficar em cima dela sem ela reagir a colisao como um objeto dinamico.
        rb.bodyType = RigidbodyType2D.Kinematic;

        target = startAtPointA ? (Vector2)pointB.position : (Vector2)pointA.position;

        if (startAtPointA) transform.position = pointA.position;
        else transform.position = pointB.position;
    }

    void FixedUpdate()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Vector2.Distance(rb.position, target) < 0.05f)
        {
            target = target == (Vector2)pointA.position ? (Vector2)pointB.position : (Vector2)pointA.position;
        }
    }

    // Faz o player "grudar" na plataforma quando estiver em cima, andando junto com ela
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    // Desenha os pontos A e B na Scene view para facilitar posicionamento
    private void OnDrawGizmos()
    {
        if (pointA == null || pointB == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pointA.position, pointB.position);
        Gizmos.DrawWireSphere(pointA.position, 0.2f);
        Gizmos.DrawWireSphere(pointB.position, 0.2f);
    }
}
