using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteToque : MonoBehaviour
{
    
    public GameObject panelMuerte;

    public GameObject jugador;
    
    public GameObject gameManager;


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            panelMuerte.SetActive(true);


            gameManager.GetComponent<MenuPausa>().enabled = false;


        }




    }



    
}
