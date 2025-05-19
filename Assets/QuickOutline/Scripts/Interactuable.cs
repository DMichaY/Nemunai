using UnityEngine;

public class Interactuable : MonoBehaviour
{
    [Header("Configuración")]
    public float distanciaActivacion = 3f;

    private Outline outline;
    private Transform jugador;

    void Awake()
    {
        outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
    }

    public void AsignarJugador(Transform referenciaJugador)
    {
        jugador = referenciaJugador;
    }

    void Update()
    {
        if (jugador == null || outline == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);
        outline.enabled = distancia <= distanciaActivacion;
    }
}
