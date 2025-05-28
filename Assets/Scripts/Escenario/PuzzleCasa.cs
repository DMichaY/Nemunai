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

    public GameObject jugador;

    public GameObject comentarioRuido;

    public GameObject comentarioCuadroMitad;

    public GameObject comentarioCuadroInicio;

    public GameObject comentarioPista;

    public GameObject Musica;

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


    public AudioClip sonidoLibro;

    AudioSource audioFuente;



    public void Start()
    {
        llave.SetActive(false);

        audioFuente = this.GetComponent<AudioSource>();

    }


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


                if (evento == false)
                {
                    InicarEvento();
                }

                else
                {

                    Eventos();

                }



                if (cuadro && contador == 5)
                {
                    comentarioCuadroMitad.transform.position = jugador.transform.position;

                    comentarioCuadroMitad.SetActive(true);

                }


                if (contador % 2 == 0)
                {
                    Musica.GetComponent<AudioSource>().pitch = Musica.GetComponent<AudioSource>().pitch + 0.1f;
                }


                if (contador == 1)
                {
                    comentarioPista.transform.position = jugador.transform.position;

                    comentarioPista.SetActive(true);
                    
                }


                





            }
        }
        
    }

    public void LeerNota()
    {
        puertasAbren = true;

        leyendo = true;

        audioFuente.PlayOneShot(sonidoLibro);     

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
                    //4 salon
                    case 4:

                        this.GetComponent<Anastasia>().Salon();

                        evento = true;

                        diabolica = true;

                        break;

                       
                    //5 brasero
                    case 5:

                        EventoRandom(i);

                        
                        break;

                    //6 vater
                    case 6:

                        EventoRandom(i);



                        break;

                    //7 baño
                    case 7:

                        EventoRandom(i);


                        break;

                    //8 sala naranja
                    case 8:

                        this.GetComponent<Cuadro>().Naranja();

                        evento = true;

                        cuadro = true;

                        comentarioCuadroInicio.transform.position = jugador.transform.position;

                        comentarioCuadroInicio.SetActive(true);

                        break;

                    //9 cocina
                    case 9:

                        this.GetComponent<Sangre>().Cocina();

                        evento = true;

                        sangre = true;

                        
                        break;

                    //11 dormitorio
                    case 11:

                        EventoRandom(i);
                        
                        break;

                    default:
                        break;
                }
            }
        }

    }

    public void EventoRandom(int numCamara)
    {

        int numero = Random.Range(1, 4);


        if (numero == 1)
        {
            evento = true;

            diabolica = true;

            if (numCamara == 11)
            {
                this.GetComponent<Anastasia>().Dormitorio();
            }

            if (numCamara == 5)
            {
                this.GetComponent<Anastasia>().Brasero();
            }


        }

        else if (numero == 2)
        {

            evento = true;

            cuadro = true;

            if (numCamara == 11)
            {
                this.GetComponent<Cuadro>().Dormitorio();

                comentarioCuadroInicio.transform.position = jugador.transform.position;

                comentarioCuadroInicio.SetActive(true);
            }

            if (numCamara == 5)
            {
                this.GetComponent<Cuadro>().Brasero();

                comentarioCuadroInicio.transform.position = jugador.transform.position;

                comentarioCuadroInicio.SetActive(true);
            }
        }

        else if (numero == 3)
        {
            this.GetComponent<Sangre>().Cocina();

            evento = true;

            sangre = true;


            comentarioRuido.transform.position = jugador.transform.position;

            comentarioRuido.SetActive(true);
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

                        if (sangre)
                        {
                            this.GetComponent<Sangre>().Banyo();
                        }

                        if (cuadro)
                        {
                            this.GetComponent<Cuadro>().Salon();
                        }

                        if (diabolica)
                        {
                            this.GetComponent<Anastasia>().Salon();
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

                    //8 sala naranja
                    case 8:

                        if (sangre)
                        {
                            this.GetComponent<Sangre>().Naranja();
                        }

                        if (cuadro)
                        {
                            this.GetComponent<Cuadro>().Naranja();
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

                        if (diabolica)
                        {
                            this.GetComponent<Anastasia>().Dormitorio();
                        }

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



    

