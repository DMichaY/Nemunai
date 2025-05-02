using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibroPueblo : Interactable
{

    GameObject manager;


    void Start()
    {

        manager = GameObject.Find("GameManager");
 

    }

    public override void Interact()
    {
       
        manager.GetComponent<PuzzleLibro>().LeerLibro(); 
            
        Destroy(this.gameObject);


        //Pausar
        Time.timeScale = 0f;

        manager.GetComponent<MenuPausa>().pausado = true;
    }
}
