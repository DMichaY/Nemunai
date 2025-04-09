using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notas : MonoBehaviour
{

  
    GameObject manager;

    

    void Start()
    {

        manager = GameObject.Find("GameManager");
 

    }

    

    void OnTriggerStay(Collider jugador)
    {
       
            if (Input.GetKeyDown(KeyCode.E))
            {

            manager.GetComponent<PuzzleCasa>().LeerNota(); 
            
            Destroy(this.gameObject);


            //Pausar
            Time.timeScale = 0f;

            manager.GetComponent<MenuPausa>().pausado = true;

         
            }


        
    }
}
