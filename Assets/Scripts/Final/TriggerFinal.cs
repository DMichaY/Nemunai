using UnityEngine;

public class TriggerFinal : MonoBehaviour
{
    public GameObject canvasFinal; // El apartado del canvas que quieres activar (FinalHuida, FinalBueno, etc.)
    public string nombreEscena;    // Nombre de la escena a cargar si el jugador pulsa "Sí"

    private bool jugadorDentro = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasFinal.SetActive(true);
            jugadorDentro = true;

            // Informamos al controlador global del canvas
            ControladorCanvasFinal controlador = canvasFinal.GetComponent<ControladorCanvasFinal>();
            if (controlador != null)
                controlador.Configurar(nombreEscena, canvasFinal);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && jugadorDentro)
        {
            canvasFinal.SetActive(false);
            jugadorDentro = false;
        }
    }
}
