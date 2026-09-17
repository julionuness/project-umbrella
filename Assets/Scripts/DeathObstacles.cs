using UnityEngine;

// Coloque num GameObject com Collider2D (Is Trigger = true) representando o obstaculo.
public class DeathObstacles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            GameOverManager.Instance.TriggerGameOver();
        }
    }
}
