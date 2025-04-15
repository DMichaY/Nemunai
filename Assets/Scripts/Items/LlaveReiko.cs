using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlaveReiko : Interactable
{
        GameObject manager;

        public GameObject llaveInventario;

    void Start()
    {

        manager = GameObject.Find("GameManager");
 

    }



    public override void Interact()
    {
        manager.GetComponent<PuzzleCasa>().llaveRecogida = true; 

        llaveInventario.SetActive(true);
            
        Destroy(this.gameObject);
    }
}
