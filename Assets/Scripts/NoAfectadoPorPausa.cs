using UnityEngine;

public class NoAfectadoPorPausa : MonoBehaviour
{
    // Puedes agregar un parámetro para realizar alguna acción en cada frame
    // sin que se vea afectado por el tiempo global
    public bool actualizarIndependientemente = true;  // Si se debe actualizar de forma independiente

    private void Update()
    {
        if (actualizarIndependientemente)
        {
            // Aquí puedes poner cualquier comportamiento que quieres que ocurra sin importar Time.timeScale
            RealizarAccion();
        }
    }

    private void RealizarAccion()
    {
        // Aquí se puede poner lo que se quiera hacer en cada frame sin que se afecte el Time.timeScale
        // Usa Time.unscaledDeltaTime para que no se vea afectado por la pausa
        float tiempoSinPausa = Time.unscaledDeltaTime;

        // Ejemplo de un movimiento que no se detendría cuando el juego está en pausa
        transform.Translate(Vector3.right * tiempoSinPausa * 5f);  // Moverse a la derecha sin pausa
    }
}
