using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaVerjaEstacion : Interactable
{
    // Variables
    Animator animacionPuertaVerja;
    public GameObject llaveVerja;
    public GameObject verja;
    public GameObject muroVerja;
    public GameObject brillitoLlave;

    public bool tieneLlave;
    public bool llaveUsada;
    public bool antiSpam;

    public GameObject imagenLlave;
    GameObject recordarLlave;

    void Start()
    {
        animacionPuertaVerja = verja.GetComponent<Animator>();
        recordarLlave = GameObject.Find("RecordatorioLlave");

        recordarLlave.SetActive(false);

        tieneLlave = false;
        llaveUsada = false;
        antiSpam = false;

        brillitoLlave.SetActive(false);
    }

    public override void Interact()
    {
        // Si se acerca Kaito a la puerta, al pulsar E interactuará con ella
        // Sin llave: Aviso buscar llave (si se hace 3 veces se le mostrará un brillo guía)
        // Con llave: Abre la puerta
        if (!llaveUsada && !antiSpam)
        {
            if (tieneLlave)
            {
                animacionPuertaVerja.SetBool("usaLlave", true);
                StartCoroutine(BorrarPuerta());

                imagenLlave.SetActive(false);
                llaveUsada = true;
            }
            else
            {
                llaveVerja.SetActive(true);

                StartCoroutine(Recordatorio());
            }
        }
    }

    IEnumerator BorrarPuerta()
    {
        yield return new WaitForSeconds(3f);

        Destroy(this.gameObject);
        Destroy(verja);
        Destroy(muroVerja);
    }

    IEnumerator Recordatorio()
    {
        antiSpam = true;

        recordarLlave.SetActive(true);

        yield return new WaitForSeconds(3f);
        
        recordarLlave.SetActive(false);

        brillitoLlave.SetActive(true);

        antiSpam = false;
    }
}

