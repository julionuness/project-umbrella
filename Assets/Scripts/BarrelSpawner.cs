using UnityEngine;

// Coloque num GameObject vazio posicionado no topo da ladeira.
public class BarrelSpawner : MonoBehaviour
{
    [Header("Barril")]
    [SerializeField] private GameObject barrelPrefab;
    [SerializeField] private float spawnInterval = 1.5f;

    [Header("Impulso inicial (empurrao pra comecar a descer a ladeira)")]
    [SerializeField] private Vector2 initialForce = new Vector2(2f, 0f);

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnBarrel();
        }
    }

    private void SpawnBarrel()
    {
        if (barrelPrefab == null) return;

        GameObject barrel = Instantiate(barrelPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = barrel.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(initialForce, ForceMode2D.Impulse);
        }
    }
}
