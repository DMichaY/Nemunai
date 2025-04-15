using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenaDemo : MonoBehaviour
{
    [Tooltip("Nombre de la escena a cargar")]
    public string nombreEscenaDestino;

    private void Start()
    {
        // Escoge aleatoriamente entre 5 o 10 segundos
        int tiempoAleatorio = Random.value < 0.5f ? 5 : 10;
        Invoke("CambiarEscena", tiempoAleatorio);
    }

    void CambiarEscena()
    {
        if (!string.IsNullOrEmpty(nombreEscenaDestino))
        {
            SceneManager.LoadScene(nombreEscenaDestino);
        }
        else
        {
            Debug.LogWarning("No se ha asignado el nombre de la escena destino.");
        }
    }
}