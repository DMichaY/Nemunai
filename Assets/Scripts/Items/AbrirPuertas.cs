using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertas : Interactable
{
    public Transform jugador; // Referencia al jugador
    public float distanciaMinima = 0f; // Distancia minima para activar
    public KeyCode teclaActivar = KeyCode.E; // Tecla para activar

    public Vector3 rotacionFinal; // en grados (Euler)

    public float duracionMovimiento = 0f;

    private bool enMovimiento = false;


    GameObject manager;

    void Start()
    {

        manager = GameObject.Find("GameManager");


    }

    public override void Interact()
    {

        float distancia = Vector3.Distance(jugador.position, transform.position);


        if (!enMovimiento && manager.GetComponent<PuzzleCasa>().puertasAbren)
        {
            StartCoroutine(Abrir());

        }

        else if (!enMovimiento)
        {
            if (manager.GetComponent<KaitoDialogo>().textoVisible == false)
            {
                manager.GetComponent<KaitoDialogo>().MostrarSiguienteTexto();
            }

            
        }


    }

    IEnumerator Abrir()
    {
        enMovimiento = true;

        Quaternion rotacionInicial = transform.rotation;

        Quaternion rotacionObjetivo = Quaternion.Euler(rotacionFinal);

        float tiempo = 0f;

        while (tiempo < duracionMovimiento)
        {
            tiempo += Time.deltaTime;
            float t = Mathf.Clamp01(tiempo / duracionMovimiento);

            transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionObjetivo, t);

            yield return null;
        }

        // Asegura que queda exactamente en el destino
        transform.rotation = rotacionObjetivo;

        enMovimiento = false;
    }

}
