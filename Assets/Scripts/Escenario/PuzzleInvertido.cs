using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Añadido para poder cambiar de escena

public class PuzzleInvertido : MonoBehaviour
{
    public GameObject boton1;
    public GameObject boton2;
    public GameObject boton3;

    public GameObject[] botones = new GameObject[7];

    int contador = 0;

    public Image pantallaNegra;

    public GameObject libro;

    public GameObject jugador;

    public string nombreEscenaDestino; // Puedes asignar el nombre desde el inspector

    public void Pulsar1()
    {
        botones[0].GetComponent<TextMeshProUGUI>().color = Color.green;
        boton1.GetComponent<Button>().interactable = false;
        contador = 1;
    }

    public void Pulsar2()
    {
        if (contador == 1)
        {
            botones[1].GetComponent<TextMeshProUGUI>().color = Color.green;
            boton2.GetComponent<Button>().interactable = false;
            contador = 2;
        }
        else
        {
            PulsarMal();
        }
    }

    public void Pulsar3()
    {
        if (contador == 2)
        {
            botones[2].GetComponent<TextMeshProUGUI>().color = Color.green;
            boton3.GetComponent<Button>().interactable = false;

            jugador.GetComponent<KaitoMovimiento>().enabled = true;

            if (pantallaNegra != null)
                pantallaNegra.CrossFadeAlpha(0, 2, false);
            else
                Debug.LogWarning("No hay pantalla negra!");

            libro.SetActive(false);

            // Cambiar a la escena deseada
            SceneManager.LoadScene(nombreEscenaDestino);
        }
        else
        {
            PulsarMal();
        }
    }

    public void PulsarMal()
    {
        foreach (var boton in botones)
        {
            boton.GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        boton1.GetComponent<Button>().interactable = true;
        boton2.GetComponent<Button>().interactable = true;
        boton3.GetComponent<Button>().interactable = true;

        contador = 0;
    }
}
