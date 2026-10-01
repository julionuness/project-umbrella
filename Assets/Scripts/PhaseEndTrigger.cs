using UnityEngine;

// Zona invisivel no fim da fase. Substitui o antigo pickup do guarda-chuva:
// mesma posicao/colisor, mesmo fluxo de vitoria, sem elemento visual.
public class PhaseEndTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            if (VictoryManager.Instance == null)
            {
                return;
            }

            VictoryManager.Instance.TriggerVictory();
        }
    }
}
