using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaTorre : Interactable
{

    public GameObject jugador;

    public GameObject puzzle;

    public GameObject blackScreen;

   
    public override void Interact()
    {
        puzzle.SetActive(true);

        blackScreen.SetActive(true);


        jugador.GetComponent<KaitoMovimiento>().enabled = false;


    }
}
