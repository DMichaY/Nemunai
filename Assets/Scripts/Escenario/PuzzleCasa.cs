using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleCasa : MonoBehaviour
{

    public GameObject[] listaNotas = new GameObject[8];

    public GameObject[] camaras = new GameObject[13];

    public GameObject libro;

    public GameObject libroInventario;

    public GameObject llave;

    public GameObject mensajeESC;

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

                mensajeESC.SetActive(false);

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

        mensajeESC.SetActive(true);

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

            mensajeESC.SetActive(false);

            listaNotas[contador - 1].SetActive(false);

            leyendo = false;


        }

        else
        {
            libro.SetActive(true);

            mensajeESC.SetActive(true);

            listaNotas[contador - 1].SetActive(true);

            leyendo = true;
        }


    }


    public void Eventos()
    {
        for (int i = 0; i < camaras.Length; i++)
        {

            if (camaras[i].activeSelf)
            {
                //4 salon
                //5 brasero
                //6 vater
                //7 baño
                //8 sala naranja
                //9 cocina
                //12 dormitorio






               
            }
            
        }

    }

}



    

