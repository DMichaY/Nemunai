using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuertaTorre : MonoBehaviour
{

    public GameObject jugador;

    public GameObject puzzle;

    public GameObject blackScreen;


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            puzzle.SetActive(true);

            blackScreen.SetActive(true);



            if (blackScreen != null)
            {
                blackScreen.GetComponent<Image>().CrossFadeAlpha(1, 2, false);

            }
            else
            {
                Debug.LogWarning("No hay pantalla negra!");

            }
                


            jugador.GetComponent<KaitoMovimiento>().enabled = false;
        }


        



    }

   
}
