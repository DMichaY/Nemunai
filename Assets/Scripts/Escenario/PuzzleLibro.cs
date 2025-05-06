using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleLibro : MonoBehaviour
{

    public GameObject[]listaNotas = new GameObject[6];

    public GameObject libro;

    public GameObject sigBoton;

    
    public int contador = 0;


    public Image pantallaNegra;

    public string nombreEscena;



    public void LeerLibro()
    {
        libro.SetActive(true);

        sigBoton.SetActive(true);

        listaNotas[contador].SetActive(true);


    }

    public void BotonSiguiente()
    {

        
        if (contador <= 4)
        {
            listaNotas[contador].SetActive(false);

            contador++;

            listaNotas[contador].SetActive(true);
        }


        else if (contador >= 5)
        {

            Time.timeScale = 1f;

            this.GetComponent<MenuPausa>().pausado = false;


            StartCoroutine("SceneLoaderCoroutine");

            libro.SetActive(false);

            listaNotas[contador].SetActive(false);


        }


    }


    private IEnumerator SceneLoaderCoroutine()
    {
        if (pantallaNegra != null) pantallaNegra.CrossFadeAlpha(1, 2, false);
        else Debug.LogWarning("No hay pantalla negra!");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nombreEscena);
    }






}
