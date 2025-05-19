using UnityEngine;

public class InteractableHighlighter : MonoBehaviour
{
    void Start()
    {
        // Buscar automáticamente todos los objetos interactuables en la escena
        Interactuable[] interactuables = FindObjectsOfType<Interactuable>();

        foreach (Interactuable interactuable in interactuables)
        {
            interactuable.AsignarJugador(transform);
        }
    }
}
