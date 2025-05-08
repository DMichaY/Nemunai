using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntBancos : Interactable
{
    // Booleano anti-spam
    LogicaVerjaEstacion puertaEstacion;

    // Texto
    public GameObject textoBancos;

    void Start()
    {
        puertaEstacion = GameObject.Find("TriggerPuerta").GetComponent<LogicaVerjaEstacion>();
        textoBancos.SetActive(false);
    }

    public override void Interact()
    {
        // En caso de que no haya ningún mensaje dispuesto en pantalla, se mostrara en pantalla
        // el mensaje de interacción
        Debug.Log("AYUDA1!!");
        
        if (!puertaEstacion.antiSpam)
        {
            StartCoroutine(IntMensaje());
        }
    }

    IEnumerator IntMensaje()
    {
        puertaEstacion.antiSpam = true;

        textoBancos.SetActive(true);

        yield return new WaitForSeconds(3f);
        
        textoBancos.SetActive(false);

        puertaEstacion.antiSpam = false;
    }
}
