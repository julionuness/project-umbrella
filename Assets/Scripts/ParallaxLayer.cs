using UnityEngine;

// Coloque este script em cada camada de fundo (ceu, montanhas, arvores distantes, etc)
public class ParallaxLayer : MonoBehaviour
{
    [Header("Referencia")]
    [SerializeField] private Transform cameraTransform; // Arraste a Main Camera

    [Header("Velocidade de Parallax")]
    [Tooltip("0 = fundo fixo (nao se move). 1 = se move junto com a camera (nao parece distante). " +
             "Valores tipicos: ceu=0.1, montanhas=0.3, arvores=0.6")]
    [SerializeField] private float parallaxFactorX = 0.5f;
    [SerializeField] private float parallaxFactorY = 0f;

    private Vector3 lastCameraPosition;

    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        transform.position += new Vector3(
            deltaMovement.x * parallaxFactorX,
            deltaMovement.y * parallaxFactorY,
            0f);

        lastCameraPosition = cameraTransform.position;
    }
}
