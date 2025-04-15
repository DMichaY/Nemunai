using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    public GameObject menu;

    public GameObject opciones;

    public GameObject idiomas;


    //---MENU INICIO---
    //boton Jugar
    public void Jugar()
    {
        //estacion
        SceneManager.LoadScene("Estacion");

    }

    //boton opciones
    public void Opciones()
    {
        menu.SetActive(false);

        idiomas.SetActive(false);

        opciones.SetActive(true);

    }


    //boton salir
    public void Salir()
    {
        Application.Quit();
    }


    //boton ITCH.IO
    public void Itch()
    {
        Application.OpenURL("https://dmichay.itch.io/");
    }

    //---MENU OPCIONES---

    //boton atras en el apartado de opciones
    public void Idiomas()
    {
        opciones.SetActive(false);

        menu.SetActive(false);

        idiomas.SetActive(true);
    }

    public void AtrasOpciones()
    {
        opciones.SetActive(false);

        idiomas.SetActive(false);

        menu.SetActive(true);
    }


    //---MENU IDIOMAS---


    //boton atras en el apartado de idiomas
    public void AtrasIdiomas()
    {
        idiomas.SetActive(false);

        menu.SetActive(false);

        opciones.SetActive(true);
    }

}
