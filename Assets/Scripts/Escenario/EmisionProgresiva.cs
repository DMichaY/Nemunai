using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EmisionProgresiva : MonoBehaviour
{
    [Header("Configuración del efecto de emisión y visibilidad")]
    public Transform jugador;                   // Referencia al jugador
    public float distanciaMinima = 5f;          // Distancia para emisión máxima
    public float intensidadMaxima = 5f;         // Emisión máxima
    public Color colorEmision = Color.white;    // Color base de emisión
    public Color colorBase = new Color(1, 1, 1, 0); // Color inicial (blanco invisible)

    private Renderer rend;
    private Material matInstancia;
    private float distanciaActual;

    void Start()
    {
        rend = GetComponent<Renderer>();
        matInstancia = rend.material;

        matInstancia.EnableKeyword("_EMISSION");
        matInstancia.SetColor("_EmissionColor", Color.black);
        matInstancia.color = colorBase;
    }

    void Update()
    {
        if (jugador == null) return;

        distanciaActual = Vector3.Distance(jugador.position, transform.position);

        if (distanciaActual <= distanciaMinima)
        {
            float factor = 1 - (distanciaActual / distanciaMinima);

            // Aumenta emisión
            Color emisionFinal = colorEmision * Mathf.Lerp(0f, intensidadMaxima, factor);
            matInstancia.SetColor("_EmissionColor", emisionFinal);

            // Aumenta visibilidad (opacidad)
            Color colorVisible = colorBase;
            colorVisible.a = Mathf.Lerp(0f, 1f, factor); // De invisible a totalmente visible
            matInstancia.color = colorVisible;
        }
        else
        {
            // Sin emisión ni visibilidad
            matInstancia.SetColor("_EmissionColor", Color.black);

            Color colorInvisible = colorBase;
            colorInvisible.a = 0f;
            matInstancia.color = colorInvisible;
        }
    }
}
