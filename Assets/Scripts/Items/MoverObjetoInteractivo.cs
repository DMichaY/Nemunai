using UnityEngine;
using System.Collections;

public class MoverObjetoInteractivo : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    public float distanciaMinima = 0f; // Distancia minima para activar
    public KeyCode teclaActivar = KeyCode.E; // Tecla para activar

    public Vector3 posicionFinal;
    public Vector3 rotacionFinal; // en grados (Euler)

    public Vector3 rotacionInicial; // en grados (Euler)
    public float duracionMovimiento = 0f;

    public GameObject texto;

    private bool enMovimiento = false;

    private bool abierto = false;

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(jugador.position, transform.position);

/*Mostrar texto "Pulsa E para abrir"
        if (distancia <= distanciaMinima)
        {

            texto.SetActive(true);

        }

        else
        {
            texto.SetActive(false);
        }
*/
        if (distancia <= distanciaMinima && Input.GetKeyDown(teclaActivar) && !enMovimiento)
        {

            
            //StartCoroutine(MoverYRotar());

            if (abierto)
            {
               StartCoroutine(Cerrar());

               abierto = false;
            }

            else
            {
                StartCoroutine(Abrir());

                abierto = true;
            }

                  
        }

        
    }
/*
    IEnumerator MoverYRotar()
    {
        enMovimiento = true;

        Vector3 posicionInicial = transform.position;
        Quaternion rotacionInicial = transform.rotation;

        Quaternion rotacionObjetivo = Quaternion.Euler(rotacionFinal);

        float tiempo = 0f;

        while (tiempo < duracionMovimiento)
        {
            tiempo += Time.deltaTime;
            float t = Mathf.Clamp01(tiempo / duracionMovimiento);

            transform.position = Vector3.Lerp(posicionInicial, posicionFinal, t);
            transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionObjetivo, t);

            yield return null;
        }

        // Asegura que queda exactamente en el destino
        transform.position = posicionFinal;
        transform.rotation = rotacionObjetivo;

        enMovimiento = false;
    }
*/

     IEnumerator Abrir()
     {
        enMovimiento = true;
        
        Quaternion inicio = Quaternion.Euler(rotacionInicial);
        
        Quaternion fin = Quaternion.Euler(rotacionFinal);
        
        float tiempo = 0f;

        while (tiempo < duracionMovimiento)
        {
            tiempo += Time.deltaTime;
            float t = Mathf.Clamp01(tiempo / duracionMovimiento);

            
            transform.rotation = Quaternion.Lerp(inicio, fin, t);


            yield return null;
        }

        // Asegura que queda exactamente en el destino
        //transform.rotation = rotacionObjetivo;

        enMovimiento = false;

        
     }

     IEnumerator Cerrar()
     {
        enMovimiento = true;
        
        Quaternion inicio = Quaternion.Euler(rotacionInicial);
        
        Quaternion fin = Quaternion.Euler(rotacionFinal);
        
        float tiempo = 0f;

        while (tiempo < duracionMovimiento)
        {
            tiempo += Time.deltaTime;
            float t = Mathf.Clamp01(tiempo / duracionMovimiento);

            
            transform.rotation = Quaternion.Lerp(fin, inicio, t);


            yield return null;
        }

        // Asegura que queda exactamente en el destino
        //transform.rotation = rotacionObjetivo;

        enMovimiento = false;

        
     }
}
