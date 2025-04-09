using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertaJaponesa : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    public float distanciaMinima = 0f; // Distancia minima para activar
    public KeyCode teclaActivar = KeyCode.E; // Tecla para activar

    //public Vector3 posicionPrimera;


    public Vector3 posicionFinal;
    

    public float duracionMovimiento = 0f;

    private bool enMovimiento = false;

    GameObject manager;

    void Start()
    {

        manager = GameObject.Find("GameManager");


    }


    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(jugador.position, transform.position);


        if (distancia <= distanciaMinima && Input.GetKeyDown(teclaActivar) && !enMovimiento && manager.GetComponent<PuzzleCasa>().puertasAbren)
        {
            StartCoroutine(Mover());


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
