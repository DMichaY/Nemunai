using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlaveReiko : Interactable
{
        GameObject manager;

        public GameObject llaveInventario;

        public GameObject Musica;

    void Start()
    {

        manager = GameObject.Find("GameManager");

        
 

    }



    public override void Interact()
    {
        manager.GetComponent<PuzzleCasa>().llaveRecogida = true; 

        llaveInventario.SetActive(true);

        if (manager.GetComponent<PuzzleCasa>().diabolica)
        {
            manager.GetComponent<Anastasia>().Final();

        }

        if (manager.GetComponent<PuzzleCasa>().sangre)
        {
            manager.GetComponent<Sangre>().Final();

        }


        if (manager.GetComponent<PuzzleCasa>().cuadro)
        {
            manager.GetComponent<Cuadro>().Final();

        }


        Musica.GetComponent<AudioSource>().Stop();

            
        Destroy(this.gameObject);
    }
}
