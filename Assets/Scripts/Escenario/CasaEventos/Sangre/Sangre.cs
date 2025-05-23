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

    //nevera moviendose
    public GameObject neveraSangre;

    public GameObject sangreNaranja;

    public GameObject sangreBrasero;

    // Start is called before the first frame update
    void Start()
    {
        neveraSangre.SetActive(false);

        sangreNaranja.SetActive(false);

    }

    public void Cocina()
    {

        nevera.SetActive(false);

        puerta1.SetActive(false);

        puerta2.SetActive(false);

        neveraSangre.SetActive(true);


    }

    public void Naranja()
    {
        neveraSangre.SetActive(false);

        sangreNaranja.SetActive(true);


        armario.SetActive(false);

        armario1.SetActive(false);

        armario2.SetActive(false);

    }

    public void Brasero()
    {
        neveraSangre.SetActive(false);

        brasero.SetActive(false);

        sangreBrasero.SetActive(true);


    }




}
