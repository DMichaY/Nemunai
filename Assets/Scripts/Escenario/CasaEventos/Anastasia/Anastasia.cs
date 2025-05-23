using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anastasia : MonoBehaviour
{

    public GameObject anastasiaSalon;

    public GameObject anastasiaDormitorio;

    public GameObject anastasiaBrasero;

    public GameObject anastasiaCocina;


    public void Dormitorio()
    {
        anastasiaDormitorio.SetActive(false);

    }


    public void Salon()
    {
        anastasiaSalon.SetActive(true);


        anastasiaBrasero.SetActive(false);

        anastasiaCocina.SetActive(false);

    }

   

    public void Brasero()
    {
        anastasiaBrasero.SetActive(true);


        anastasiaSalon.SetActive(false);

        anastasiaCocina.SetActive(false);


    }


    public void Cocina()
    {
        anastasiaCocina.SetActive(true);


        anastasiaBrasero.SetActive(false);

        anastasiaSalon.SetActive(false);

    }


    public void Final()
    {
        anastasiaCocina.SetActive(false);


        anastasiaBrasero.SetActive(false);

        anastasiaSalon.SetActive(false);

    }










}



