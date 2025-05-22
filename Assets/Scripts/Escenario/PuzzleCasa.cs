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

    //eventos
    public bool evento = false;

    public bool diabolica = false;

    public bool cuadro = false;

    public bool sangre = false;

    public Image pantallaNegra;


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

        if (evento == false)
        {
            InicarEvento();
        }

        else
        {

            Eventos();

        }

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


    //Iniciar diferente evento
    public void InicarEvento()
    {
        for (int i = 0; i < camaras.Length; i++)
        {

            if (camaras[i].activeSelf)
            {
                //4 **Muñeca diabolica**
                //8 **Cuadro poseido**
                //9 **masa sangrienta**
              
                switch (i)
                {
                    case 4:
                        
                        this.GetComponent<Anastasia>().Salon();

                        evento = true;

                        diabolica = true;

                        break;

                    case 8:

                       

                        evento = true;

                        break;

                    case 9:

                        

                        evento = true;

                        break;



                    default:
                        break;
                }
            }
        }

    }


    public void Eventos()
    {
        for (int i = 0; i < camaras.Length; i++)
        {

            if (camaras[i].activeSelf)
            {

                switch (i)
                {
                    //4 salon
                    case 4:

                        if (cuadro)
                        {

                        }


                        if (sangre)
                        {

                        }



                        break;

                    //5 brasero
                    case 5:

                        if (cuadro)
                        {

                        }


                        if (sangre)
                        {

                        }


                        if (diabolica)
                        {
                            this.GetComponent<Anastasia>().Brasero();
                        }



                        break;

                    //6 vater
                    case 6:



                        break;

                    //7 baño
                    case 7:



                        break;

                    //8 sala naranja
                    case 8:



                        break;

                    //9 cocina
                    case 9:



                        break;

                    //12 dormitorio
                    case 11:



                        break;




                    default:
                        break;
                }

            }

        }


    }

}



    

