using UnityEngine;

public class ActivadorDeObjeto : MonoBehaviour
{
    public Interactuable objetoADesbloquear; // El objeto que se desbloquea (ej. caja)
    public Transform jugador; // El modelo del jugador
    public float distanciaParaActivar = 3f;

    private bool yaActivado = false;

    void Update()
    {
        if (yaActivado || jugador == null || objetoADesbloquear == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= distanciaParaActivar)
        {
            objetoADesbloquear.Activar();
            yaActivado = true;
            Debug.Log("Objeto desbloqueado automáticamente por cercanía.");
        }
    }
}
