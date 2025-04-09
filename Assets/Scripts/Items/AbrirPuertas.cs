using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertas : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    public float distanciaMinima = 0f; // Distancia minima para activar
    public KeyCode teclaActivar = KeyCode.E; // Tecla para activar

    //public Vector3 posicionFinal;
    public Vector3 rotacionFinal; // en grados (Euler)

    public float duracionMovimiento = 0f;

    private bool enMovimiento = false;


    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(jugador.position, transform.position);


        if (distancia <= distanciaMinima && Input.GetKeyDown(teclaActivar) && !enMovimiento)
        {
            StartCoroutine(Abrir());

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
