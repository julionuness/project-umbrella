using UnityEngine;

// Coloque no PREFAB do barril. Precisa de Rigidbody2D (Dynamic) + Collider2D (NAO trigger,
// para rolar de verdade fisicamente contra o chao/ladeira).
[RequireComponent(typeof(Rigidbody2D))]
public class Barrel : MonoBehaviour
{
    [SerializeField] private float lifeTime = 8f; // Autodestroi mesmo se nao sair da tela

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameOverManager.Instance.TriggerGameOver();
        }
    }

    // Limpa o barril assim que ele sai da area visivel da camera (ex: caiu no limbo ou passou do mapa)
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
