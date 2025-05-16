using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibroPueblo : Interactable
{

    GameObject manager;

    public GameObject jugador;


    void Start()
    {

        manager = GameObject.Find("GameManager");
 

    }

    public override void Interact()
    {
       
        manager.GetComponent<PuzzleLibro>().LeerLibro();

        jugador.GetComponent<KaitoMovimiento>().enabled = false;


    }
}
