using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLibro : MonoBehaviour
{

    public GameObject[]listaNotas = new GameObject[6];

    public GameObject libro;

    public GameObject sigBoton;

    
    public int contador = 0;


    public void LeerLibro()
    {
        libro.SetActive(true);

        sigBoton.SetActive(true);

        listaNotas[contador].SetActive(true);


    }

    public void BotonSiguiente()
    {
        
        if (contador <= 4)
        {
            listaNotas[contador].SetActive(false);

            contador++;

            listaNotas[contador].SetActive(true);
        }


        else if (contador >= 5)
        {
            Debug.Log("pelea reiko");
        }


    }



}
