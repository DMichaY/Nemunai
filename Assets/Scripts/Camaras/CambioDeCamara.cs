using UnityEngine;

public class CambioDeCamara : MonoBehaviour
{
    public Camera camaraActivar;  // Cámara a activar al entrar en este trigger
    public Camera camaraDesactivar; // Cámara a desactivar cuando entramos aquí
    private bool jugadorDentro = false; // Para evitar activaciones innecesarias

    private void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player") && !jugadorDentro)
        {
            jugadorDentro = true; // Marcamos que el jugador ya entró a esta zona

            if (camaraDesactivar != null) 
                camaraDesactivar.gameObject.SetActive(false);

            if (camaraActivar != null)
                camaraActivar.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            jugadorDentro = false; // Permitimos que pueda volver a activarse si regresa
        }
    }
}
