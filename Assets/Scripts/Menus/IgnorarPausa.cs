using UnityEngine;

public class IgnorarPausa : MonoBehaviour
{
    [Header("Velocidad de rotación en grados por segundo")]
    public float velocidadRotacion = 50f;

    void Update()
    {
        // Rotar sobre el eje Z utilizando el tiempo no escalado (ignora la pausa)
        transform.Rotate(0f, 0f, velocidadRotacion * Time.unscaledDeltaTime);
    }
}
