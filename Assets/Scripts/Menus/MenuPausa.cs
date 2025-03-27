using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{

    public GameObject pausaMenu;

    public bool pausado = false;



    // Update is called once per frame
    void Update()
    {
        //menu de pausa
        if ((Input.GetKeyDown(KeyCode.Escape)))
        {
            if (!pausado)
            {
                
                Pausa();

            }
            else
            {
                Continuar();

            }
            

        }
    }
    //pausar y mostrar menu
    public void Pausa()
    {
        pausaMenu.SetActive(true);
        Time.timeScale = 0f;
        pausado = true;
    }

    //quita la pausa
    public void Continuar()
    {
        pausaMenu.SetActive(false);
        Time.timeScale = 1f;
        pausado = false;

    }


    //boton salir
    public void Salir()
    {
        Application.Quit();
    }


    //volver al menu
    public void VolverMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");

    }


}
