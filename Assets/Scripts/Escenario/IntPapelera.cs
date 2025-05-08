using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntPapelera : Interactable
{
    // Booleano anti-spam
    LogicaVerjaEstacion puertaEstacion;

    // Texto
    public GameObject textoPapelera;

    void Start()
    {
        puertaEstacion = GameObject.Find("TriggerPuerta").GetComponent<LogicaVerjaEstacion>();
        textoPapelera.SetActive(false);
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

        textoPapelera.SetActive(true);

        yield return new WaitForSeconds(3f);
        
        textoPapelera.SetActive(false);

        puertaEstacion.antiSpam = false;
    }
}
