using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MasaNegra : MonoBehaviour
{
    [Header("Configuración del efecto")]
    public float velocidadCambio = 1f;     // Qué tan rápido cambia entre valores
    public float intensidadMin = 0.1f;     // Valor mínimo del normal map
    public float intensidadMax = 1.5f;     // Valor máximo del normal map

    private Renderer rend;
    private Material matInstancia;
    private float objetivoActual;
    private float valorActual;

    void Start()
    {
        rend = GetComponent<Renderer>();
        matInstancia = rend.material;

        // Asegura que se use la propiedad _BumpScale
        if (!matInstancia.HasProperty("_BumpScale"))
        {
            Debug.LogWarning("El material no tiene un normal map asignado o no usa _BumpScale.");
            enabled = false;
            return;
        }

        valorActual = matInstancia.GetFloat("_BumpScale");
        objetivoActual = Random.Range(intensidadMin, intensidadMax);
    }

    void Update()
    {
        // Lerp hacia el objetivo
        valorActual = Mathf.MoveTowards(valorActual, objetivoActual, velocidadCambio * Time.deltaTime);
        matInstancia.SetFloat("_BumpScale", valorActual);

        // Si llega al valor objetivo, elegimos uno nuevo
        if (Mathf.Approximately(valorActual, objetivoActual))
        {
            objetivoActual = Random.Range(intensidadMin, intensidadMax);
        }
    }
}
