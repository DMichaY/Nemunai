using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sangre : MonoBehaviour
{

    public GameObject nevera;

    public GameObject puerta1;

    public GameObject puerta2;

    public GameObject armario;

    public GameObject armario1;

    public GameObject armario2;

    public GameObject brasero;

    public GameObject madera;


    public GameObject comentario;


    public GameObject puertabrasero1;

    public GameObject puertabrasero2;

    //nevera moviendose
    public GameObject neveraSangre;

    public GameObject armarioSangre;

    public GameObject sangreEmpieza;

    public GameObject sangreBanyo;

    public GameObject sangreNaranja;

    public GameObject sangreBrasero;

   




    void Start()
    {
        neveraSangre.SetActive(false);

        sangreEmpieza.SetActive(false);

        sangreNaranja.SetActive(false);

    }

    public void Cocina()
    {

        nevera.SetActive(false);

        puerta1.SetActive(false);

        puerta2.SetActive(false);

        neveraSangre.SetActive(true);


        neveraSangre.GetComponent<AudioSource>().Play();




    }

    public void Naranja()
    {
        neveraSangre.SetActive(false);

        armario.SetActive(false);

        armario1.SetActive(false);

        armario2.SetActive(false);

        sangreNaranja.SetActive(true);

        sangreEmpieza.SetActive(true);

        armarioSangre.GetComponent<AudioSource>().Play();

    }

    public void Brasero()
    {
        neveraSangre.SetActive(false);

        brasero.SetActive(false);

        puertabrasero1.SetActive(false);

        puertabrasero2.SetActive(false);

        madera.SetActive(false);

        sangreBrasero.SetActive(true);

        sangreEmpieza.SetActive(true);

    }

    public void Banyo()
    {
        neveraSangre.SetActive(false);


        sangreBanyo.SetActive(true);

        sangreEmpieza.SetActive(true);

    }


    public void Final()
    {

        neveraSangre.SetActive(false);

        sangreEmpieza.SetActive(false);

        sangreBanyo.SetActive(false);

        sangreBrasero.SetActive(false);

        sangreNaranja.SetActive(false);

        comentario.SetActive(true);



    }


}
