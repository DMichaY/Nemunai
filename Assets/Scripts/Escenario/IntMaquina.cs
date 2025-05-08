using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntMaquina : Interactable
{
    // Booleano anti-spam
    LogicaVerjaEstacion puertaEstacion;
    
    // Texto
    public GameObject textoMaquina;

    void Start()
    {
        puertaEstacion = GameObject.Find("TriggerPuerta").GetComponent<LogicaVerjaEstacion>();
        textoMaquina.SetActive(false);
    }

    public override void Interact()
    {
        // En caso de que no haya ningún mensaje dispuesto en pantalla, se mostrara en pantalla
        // el mensaje de interacción
        if (!puertaEstacion.antiSpam)
        {
            StartCoroutine(IntMensaje3());
        }
    }

    IEnumerator IntMensaje3()
    {
        puertaEstacion.antiSpam = true;

        textoMaquina.SetActive(true);

        yield return new WaitForSeconds(3f);
        
        textoMaquina.SetActive(false);

        puertaEstacion.antiSpam = false;
    }
}
