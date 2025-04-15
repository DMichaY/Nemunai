using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuertaReiko : Interactable
{
    public Transform jugador; // Referencia al jugador
    public float distanciaMinima = 0f; // Distancia minima para activar
    public KeyCode teclaActivar = KeyCode.E; // Tecla para activar

    public Vector3 posicionFinal;
    

    public float duracionMovimiento = 0f;

    private bool enMovimiento = false;

    GameObject manager;


    void Start()
    {

        manager = GameObject.Find("GameManager");

    }

    public override void Interact()
    {
        if (jugador == null) return;

        //llave recogida
        if (!enMovimiento && manager.GetComponent<PuzzleCasa>().llaveRecogida)
        {
            StartCoroutine(Mover());

        }

        // diraio recogido
        else if (!enMovimiento && manager.GetComponent<PuzzleCasa>().puertasAbren)
        {
            if (manager.GetComponent<KaitoDialogo>().textoVisible == false)
            {
           
              manager.GetComponent<KaitoDialogo>().TextoPuertaDiario();

            }

        }

        //diario sin recoger
        else if (!enMovimiento)
        {
            if (manager.GetComponent<KaitoDialogo>().textoVisible == false)
            {


                manager.GetComponent<KaitoDialogo>().TextoPuertaReiko();


            }


        }



    }

    IEnumerator Mover()
    {
        enMovimiento = true;

        Vector3 posicionInicial = transform.position;
       

        float tiempo = 0f;

        while (tiempo < duracionMovimiento)
        {
            tiempo += Time.deltaTime;
            float t = Mathf.Clamp01(tiempo / duracionMovimiento);

             transform.position = Vector3.Lerp(posicionInicial, posicionFinal, t);
            

            yield return null;
        }
 
        transform.position = posicionFinal;


        enMovimiento = false;
    }

   

}
