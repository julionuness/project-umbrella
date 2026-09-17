using UnityEngine;

// Coloque este script num GameObject com BoxCollider2D (Is Trigger = true)
// posicionado bem embaixo do mapa, cobrindo toda a largura do nivel.
public class LimboZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameOverManager.Instance.TriggerGameOver();
        }
    }
}
