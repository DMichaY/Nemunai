using UnityEngine;

[RequireComponent(typeof(Light))]
public class LuzProgresiva : MonoBehaviour
{
    [Header("Configuración de la luz")]
    public Transform jugador;               // Referencia al jugador
    public float distanciaMinima = 5f;      // Distancia desde la que empieza a aumentar la luz
    public float intensidadMaxima = 3f;     // Intensidad máxima cuando el jugador está justo en el foco

    private Light luz;
    private float distanciaActual;

    private void Start()
    {
        luz = GetComponent<Light>();
        luz.intensity = 0f; // Empieza apagada
    }

    private void Update()
    {
        if (jugador == null) return;

        distanciaActual = Vector3.Distance(jugador.position, transform.position);

        if (distanciaActual <= distanciaMinima)
        {
            // Calcula intensidad proporcional a la distancia
            float factor = 1 - (distanciaActual / distanciaMinima);
            luz.intensity = Mathf.Lerp(0f, intensidadMaxima, factor);
        }
        else
        {
            luz.intensity = 0f; // Fuera de rango, sin luz
        }
    }
}
