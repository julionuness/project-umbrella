using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class AnvilTrap : MonoBehaviour
{
    [Header("Deteccao")]
    [SerializeField] private float detectionRadiusX = 3f; // Distancia horizontal pra ativar
    [SerializeField] private Transform player; // Opcional: se vazio, procura pela tag Player

    [Header("Comportamento")]
    [SerializeField] private float warningTime = 0.4f; // Tempo "tremendo" antes de cair
    [SerializeField] private float shakeAmount = 0.05f;

    [Header("Layers (chão só depois de pousar)")]
    [SerializeField] private string layerWhileSuspended = "Default";
    [SerializeField] private string layerAfterLanding = "Ground";

    private Rigidbody2D rb;
    private bool triggered = false;
    private bool canKill = false; // So mata enquanto esta caindo
    private Vector3 originalPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Parada ate detectar o player
        originalPosition = transform.position;

        // Enquanto suspensa, nao conta como chao (assim o player nao fica "flutuando"
        // parado em cima dela antes da queda ser ativada)
        int suspendedLayer = LayerMask.NameToLayer(layerWhileSuspended);
        if (suspendedLayer != -1) gameObject.layer = suspendedLayer;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
    }

    void Update()
    {
        if (triggered || player == null) return;

        float distX = Mathf.Abs(player.position.x - transform.position.x);
        if (distX <= detectionRadiusX)
        {
            triggered = true;
            StartCoroutine(FallRoutine());
        }
    }

    private IEnumerator FallRoutine()
    {
        float elapsed = 0f;
        while (elapsed < warningTime)
        {
            transform.position = originalPosition + (Vector3)(Random.insideUnitCircle * shakeAmount);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        rb.bodyType = RigidbodyType2D.Dynamic; // Agora cai normalmente com gravidade
        canKill = true; // Perigosa enquanto esta caindo

        StartCoroutine(CheckLanded());
    }

    private IEnumerator CheckLanded()
    {
        // Pequena espera pra garantir que ela realmente comecou a cair
        // antes de checar a velocidade (senao "pousada" ficaria true no frame 0)
        yield return new WaitForSeconds(0.15f);

        while (rb.linearVelocity.magnitude > 0.05f)
        {
            yield return null;
        }

        canKill = false; // Pousou: nao mata mais ao encostar

        // Agora que pousou, passa a servir de chao de verdade
        int groundLayer = LayerMask.NameToLayer(layerAfterLanding);
        if (groundLayer != -1) gameObject.layer = groundLayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canKill && collision.transform.CompareTag("Player"))
        {
            GameOverManager.Instance.TriggerGameOver();
        }
    }
}