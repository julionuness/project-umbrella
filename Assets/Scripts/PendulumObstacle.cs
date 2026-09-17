using UnityEngine;

// Coloque num GameObject com Collider2D (Is Trigger = true) representando o pendulo/bola de demolicao.
[RequireComponent(typeof(Rigidbody2D))]
public class PendulumObstacle : MonoBehaviour
{
    [Header("Ponto de articulacao (o topo da corrente/gancho)")]
    [SerializeField] private Transform pivotPoint;

    [Header("Configuracao do balanco")]
    [SerializeField] private float swingAngle = 45f; // graus para cada lado, a partir da posicao inicial
    [SerializeField] private float swingSpeed = 1f;   // velocidade do vai-e-vem (maior = mais rapido)

    private Rigidbody2D rb;
    private float distanceFromPivot;
    private float baseAngle; // angulo inicial entre o pivot e o objeto (geralmente ~ -90, direto pra baixo)

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        if (pivotPoint != null)
        {
            distanceFromPivot = Vector2.Distance(pivotPoint.position, transform.position);

            Vector2 dir = (Vector2)transform.position - (Vector2)pivotPoint.position;
            baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }
    }

    void FixedUpdate()
    {
        if (pivotPoint == null) return;

        // Oscila suavemente entre -swingAngle e +swingAngle usando uma onda seno
        float swingOffset = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        float totalAngleRad = (baseAngle + swingOffset) * Mathf.Deg2Rad;

        Vector2 offset = new Vector2(Mathf.Cos(totalAngleRad), Mathf.Sin(totalAngleRad)) * distanceFromPivot;
        Vector2 newPosition = (Vector2)pivotPoint.position + offset;

        rb.MovePosition(newPosition);

        // Gira o proprio objeto junto, pra dar a sensacao de balanco real (nao so translacao)
        rb.MoveRotation(swingOffset);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameOverManager.Instance.TriggerGameOver();
        }
    }
}
