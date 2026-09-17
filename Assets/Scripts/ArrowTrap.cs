using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ArrowTrap : MonoBehaviour
{
    [Header("Pontos")]
    [SerializeField] private Transform startPoint;  // De onde a flecha "sai"
    [SerializeField] private Transform targetPoint;  // O alvo (archery target)

    [Header("Configuracao")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float delayBeforeShoot = 0.5f; // Pausa "mirando" antes de disparar
    [SerializeField] private float delayBeforeLoop = 1f;    // Pausa cravada no alvo antes de reiniciar

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Start()
    {
        StartCoroutine(ShootLoop());
    }

    private IEnumerator ShootLoop()
    {
        while (true)
        {
            // Reposiciona no inicio e aponta na direcao do alvo (parece estar "mirando")
            Vector3 position = startPoint.position;
            rb.position = position;

            Vector2 direction = (targetPoint.position - startPoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            yield return new WaitForSeconds(delayBeforeShoot);

            // Voa ate o alvo
            while (Vector2.Distance(rb.position, targetPoint.position) > 0.05f)
            {
                Vector2 newPos = Vector2.MoveTowards(rb.position, targetPoint.position, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
                yield return new WaitForFixedUpdate();
            }

            // Fica cravada no alvo por um instante antes de reiniciar o loop
            yield return new WaitForSeconds(delayBeforeLoop);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameOverManager.Instance.TriggerGameOver();
        }
    }
}
