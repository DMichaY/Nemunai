using UnityEngine;

public class InteractableHighlighter : MonoBehaviour
{
    void Start()
    {
        // Buscar autom�ticamente todos los objetos interactuables en la escena
        Interactuable[] interactuables = FindObjectsOfType<Interactuable>();

        foreach (Interactuable interactuable in interactuables)
        {
            interactuable.AsignarJugador(transform);
        }
    }
}
