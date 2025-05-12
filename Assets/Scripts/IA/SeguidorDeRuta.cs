using UnityEngine;
using System.Collections.Generic;

public class SeguidorDeRuta : MonoBehaviour
{
    [Header("Ruta automática")]
    public Transform padreDePuntos;
    public float velocidad = 3f;
    public float rotacionVelocidad = 5f;
    public float distanciaMinima = 0.2f;

    private List<Transform> puntosRuta = new List<Transform>();
    private int indiceActual = 0;
    private float alturaOriginal;

    private void Start()
    {
        if (padreDePuntos == null)
        {
            Debug.LogWarning("No has asignado el padre de los puntos.");
            enabled = false;
            return;
        }

        foreach (Transform hijo in padreDePuntos)
        {
            puntosRuta.Add(hijo);
        }

        if (puntosRuta.Count == 0)
        {
            Debug.LogWarning("No se encontraron puntos hijos.");
            enabled = false;
            return;
        }

        alturaOriginal = transform.position.y;
    }

    private void Update()
    {
        if (puntosRuta.Count == 0) return;

        Vector3 destino = puntosRuta[indiceActual].position;
        destino.y = alturaOriginal;

        // Movimiento hacia el destino
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

        // Dirección ignorando altura
        Vector3 direccion = destino - transform.position;
        direccion.y = 0f;

        if (direccion != Vector3.zero)
        {
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
            float yActual = transform.eulerAngles.y;
            float yObjetivo = rotacionDeseada.eulerAngles.y;

            // Interpolación suave del ángulo Y
            float yRotSuave = Mathf.LerpAngle(yActual, yObjetivo, rotacionVelocidad * Time.deltaTime);

            // Mantener X y Z originales
            Vector3 rotacionActual = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(rotacionActual.x, yRotSuave, rotacionActual.z);
        }

        // Avanzar al siguiente punto si está cerca
        if (Vector3.Distance(transform.position, destino) <= distanciaMinima)
        {
            indiceActual++;
            if (indiceActual >= puntosRuta.Count)
            {
                indiceActual = puntosRuta.Count - 1;
                // Para bucle infinito: indiceActual = 0;
            }
        }
    }
}
