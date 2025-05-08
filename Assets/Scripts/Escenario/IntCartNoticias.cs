using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntCartNoticias : Interactable
{
    // Booleano anti-spam
    LogicaVerjaEstacion puertaEstacion;

    // Texto
    public GameObject textoNoticias;

    void Start()
    {
        puertaEstacion = GameObject.Find("TriggerPuerta").GetComponent<LogicaVerjaEstacion>();
        textoNoticias.SetActive(false);
    }

    public override void Interact()
    {
        // En caso de que no haya ningún mensaje dispuesto en pantalla, se mostrara en pantalla
        // el mensaje de interacción
        if (!puertaEstacion.antiSpam)
        {
            StartCoroutine(IntMensaje());
        }
    }

    IEnumerator IntMensaje()
    {
        puertaEstacion.antiSpam = true;

        textoNoticias.SetActive(true);

        yield return new WaitForSeconds(3f);
        
        textoNoticias.SetActive(false);

        puertaEstacion.antiSpam = false;
    }
}
