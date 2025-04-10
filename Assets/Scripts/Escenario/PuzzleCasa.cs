using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleCasa : MonoBehaviour
{

    public GameObject[]listaNotas = new GameObject[8];

    public GameObject libro;

    public GameObject libroInventario;

    public GameObject llave;

    public int contador = 0;

    public bool puertasAbren = false;

    public bool leyendo = false;

    public bool llaveRecogida = false;

    public Image pantallaNegra;


    // Start is called before the first frame update
    void Update()
    {

        if (leyendo)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                libro.SetActive(false);

                for (int i = 0; i < listaNotas.Length; i++)
                {
                    listaNotas[i].SetActive(false);
                }

                leyendo = false;

            }
        }

    }

    public void LeerNota()
    {
        puertasAbren = true;

        leyendo = true;

        libroInventario.SetActive(true);
        
        //mostrar pista
        
        libro.SetActive(true);
        
        listaNotas[contador].SetActive(true);

        //aumenta el contador para mostrar la siguiente pista la proxima vez
        contador++;

        //todas las pistas recogidas
        if (contador == 8)
        {
            llave.SetActive(true);
        }


    }

    //mostrar diario en el menu de pausa
    public void MostrarDiario()
    {

        if (leyendo)
        {
            libro.SetActive(false); 

            listaNotas[contador-1].SetActive(false);

            leyendo = false;


        }

        else
        {
            libro.SetActive(true); 

            listaNotas[contador-1].SetActive(true);

            leyendo = true;
        }





    }

}



    

