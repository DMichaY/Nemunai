using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlaveReiko : MonoBehaviour
{
        GameObject manager;

        public GameObject llaveInventario;

    void Start()
    {

        manager = GameObject.Find("GameManager");
 

    }

    

    void OnTriggerStay(Collider jugador)
    {
       
            if (Input.GetKeyDown(KeyCode.E))
            {

            manager.GetComponent<PuzzleCasa>().llaveRecogida = true; 

            llaveInventario.SetActive(true);
            
            Destroy(this.gameObject);

         
            }


        
    }
}
