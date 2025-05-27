using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadro : MonoBehaviour
{


    public GameObject sombra;

    public GameObject brasero;

    public GameObject salon;

    public GameObject naranja;

    public GameObject dormitorio;


    public GameObject comentarioFinal;


    public GameObject armarioNegro;

    public GameObject braseroCama;


    public GameObject braseroSalon;




    public GameObject armario;

    public GameObject armarioPuerta1;


    public GameObject armarioPuerta2;




    public void Start()
    {

        naranja.SetActive(false);

        dormitorio.SetActive(false);

        salon.SetActive(false);

    }



    public void Naranja()
    {
        naranja.SetActive(true);

        armario.SetActive(false);

        armarioPuerta1.SetActive(false);

        armarioPuerta2.SetActive(false);

        armarioNegro.GetComponent<AudioSource>().Play();



    }


    public void Brasero()
    {
        brasero.SetActive(true);




    }


    public void Salon()
    {
        salon.SetActive(true);

        braseroSalon.GetComponent<AudioSource>().Play();

    }


    public void Dormitorio()
    {

        dormitorio.SetActive(true);

        braseroCama.GetComponent<AudioSource>().Play();


    }

    public void Final()
    {
        dormitorio.SetActive(false);

        salon.SetActive(false);

        brasero.SetActive(false);

        naranja.SetActive(false);


        comentarioFinal.SetActive(true);

    }



}
