using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
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

            if (diabolica)
            {
                this.GetComponent<Anastasia>().Final();

            }
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
                    //dormitorio
                    case 11:
                        
                        this.GetComponent<Anastasia>().Dormitorio();

                        evento = true;

                        diabolica = true;

                        break;

                    //habitacion naranja
                    case 8:

                       this.GetComponent<Cuadro>().Naranja();

                        evento = true;

                        cuadro = true;

                        break;

                    //cocina
                    case 9:

                        this.GetComponent<Sangre>().Cocina();

                        evento = true;

                        sangre = true;

                        break;


                    //4 salon
                    case 4:

                        EventoRandom();

                        break;

                    //5 brasero
                    case 5:

                        EventoRandom();


                        break;

                    //7 baño
                    case 7:

                        EventoRandom();

                        break;

                    default:
                        break;
                }
            }
        }

    }

    public void EventoRandom()
    {

        int numero = Random.Range(1, 4);
        
     
        if (numero == 1)
        {
            this.GetComponent<Anastasia>().Dormitorio();

            evento = true;

            diabolica = true;
        }

        else if (numero == 2)
        {
            this.GetComponent<Cuadro>().Naranja();

            evento = true;

            cuadro = true;
        }

        else
        {
            this.GetComponent<Sangre>().Cocina();

            evento = true;

            sangre = true;
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

                        if (diabolica)
                        {
                            this.GetComponent<Anastasia>().Salon();
                        }

                        if (sangre)
                        {
                            this.GetComponent<Sangre>().Banyo();
                        }

                        if (cuadro)
                        {
                            this.GetComponent<Cuadro>().Salon();
                        }

                        break;

                    //5 brasero
                    case 5:

                        if (sangre)
                        {
                            this.GetComponent<Sangre>().Brasero();
                        }


                        if (diabolica)
                        {
                            this.GetComponent<Anastasia>().Brasero();
                        }


                        if (cuadro)
                        {
                            this.GetComponent<Cuadro>().Brasero();
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

                        if (sangre)
                        {
                            this.GetComponent<Sangre>().Naranja();
                        }

                        
                        break;

                    //9 cocina
                    case 9:


                        if (diabolica)
                        {
                            this.GetComponent<Anastasia>().Cocina();
                        }

                        break;

                    //11 dormitorio
                    case 11:

                        if (cuadro)
                        {
                            this.GetComponent<Cuadro>().Dormitorio();
                        }


                        break;

                    default:
                        break;
                }

            }

        }


    }

}



    

