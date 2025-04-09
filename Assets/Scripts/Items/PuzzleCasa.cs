using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCasa : MonoBehaviour
{

    public GameObject[]listaNotas = new GameObject[8];

    public int contador = 0;

    public bool puertasAbren = false;

    public GameObject libro;

    public bool leyendo = false;


    void Start()
    {

       



    }

    // Start is called before the first frame update
    void Update()
    {

        if (leyendo)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                libro.SetActive(false);

                for (int i = 0; i < listaNotas.Length; i++)
                {
                    listaNotas[i].SetActive(false);
                }

                leyendo = false;

            }

           

           
        }



    }

    public void LeerNota()
    {
        puertasAbren = true;

        leyendo = true;
        
        //mostrar pista
        
        libro.SetActive(true);
        
        listaNotas[contador].SetActive(true);

        //aumenta el contador para mostrar la siguiente pista la proxima vez
        contador++;


    }

}



    

