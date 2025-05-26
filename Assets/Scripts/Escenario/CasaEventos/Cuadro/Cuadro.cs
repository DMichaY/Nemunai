using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadro : MonoBehaviour
{


    public GameObject sombra;

    public GameObject brasero1;

    public GameObject brasero2;

    public GameObject sala1;

    public GameObject sala2;

    public GameObject sala3;

    public GameObject naranja;

    public GameObject dor1;

    public GameObject dor2;


    

  
    public void Naranja()
    {
        naranja.SetActive(true);

    }


    public void Brasero()
    {
        brasero1.SetActive(true);

        brasero2.SetActive(true);
    }


    public void Salon()
    {
        sala1.SetActive(true);

        sala2.SetActive(true);

        sala3.SetActive(true);

    }


    public void Dormitorio()
    {

        dor1.SetActive(true);
        
        dor2.SetActive(true);

        
    }



}
