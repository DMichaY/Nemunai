using UnityEngine;

public class Interactuable : MonoBehaviour
{
    [Header("Configuración")]
    public float distanciaActivacion = 3f;
    public bool activo = true; // << SOLO SE ACTIVA CUANDO SE PERMITA

    private Outline outline;
    private Transform jugador;

    private bool puedeInteractuar;


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
        if (!activo || jugador == null || outline == null) return;

        
        float distancia = Vector3.Distance(transform.position, jugador.position);
        outline.enabled = distancia <= distanciaActivacion;

        if (distancia <= distanciaActivacion && !puedeInteractuar)
        {
            puedeInteractuar = true;
            jugador.GetComponent<KaitoMovimiento>().ChangeInteractable(gameObject);
        }
        else if (puedeInteractuar)
        {
            puedeInteractuar = false;
            jugador.GetComponent<KaitoMovimiento>().ChangeInteractable(null);
        }
        
        
    }

    
    public void Activar()
    {
        activo = true;
    }

    
}
