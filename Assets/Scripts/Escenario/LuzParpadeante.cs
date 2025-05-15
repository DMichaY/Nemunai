using UnityEngine;

[RequireComponent(typeof(Light))]
public class LuzParpadeante : MonoBehaviour
{
    [Header("Configuraci�n del parpadeo")]
    [Tooltip("Duraci�n que la luz permanece apagada (segundos)")]
    public float tiempoApagada = 1.5f;

    [Tooltip("Duraci�n que tarda en encenderse (subida r�pida)")]
    public float duracionEncendido = 0.1f;

    [Tooltip("Duraci�n que tarda en apagarse (bajada lenta)")]
    public float duracionApagado = 0.5f;

    [Tooltip("Intensidad m�xima que alcanza la luz")]
    public float intensidadMaxima = 1.5f;
uz
    private enum Estado { Apagada, Encendiendo, Apagando }
    private Estado estadoActual = Estado.Apagada;

    private float tiempoEstado = 0f;
    private Light luz;

    void Start()
    {
        luz = GetComponent<Light>();
        luz.intensity = 0f;
    }

    void Update()
    {
        tiempoEstado += Time.deltaTime;

        switch (estadoActual)
        {
            case Estado.Apagada:
                luz.intensity = 0f;
                if (tiempoEstado >= tiempoApagada)
                {
                    CambiarEstado(Estado.Encendiendo);
                }
                break;

            case Estado.Encendiendo:
                float tEncendido = tiempoEstado / duracionEncendido;
                luz.intensity = Mathf.Lerp(0f, intensidadMaxima, tEncendido);
                if (tiempoEstado >= duracionEncendido)
                {
                    CambiarEstado(Estado.Apagando);
                }
                break;

            case Estado.Apagando:
                float tApagado = tiempoEstado / duracionApagado;
                luz.intensity = Mathf.Lerp(intensidadMaxima, 0f, tApagado);
                if (tiempoEstado >= duracionApagado)
                {
                    CambiarEstado(Estado.Apagada);
                }
                break;
        }
    }

    void CambiarEstado(Estado nuevoEstado)
    {
        estadoActual = nuevoEstado;
        tiempoEstado = 0f;
    }
}