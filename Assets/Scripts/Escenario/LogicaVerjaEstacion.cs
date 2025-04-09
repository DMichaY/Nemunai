using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaVerjaEstacion : MonoBehaviour
{
    // Variables
    Animator animacionPuertaVerja;
    public GameObject llaveVerja;
    public GameObject verja;
    public GameObject muroVerja;
    public GameObject brillitoLlave;

    public int interacciones = 0;

    public bool tieneLlave;
    public bool llaveUsada;
    public bool antiSpam;

    public GameObject imagenLlave;
    GameObject recordarLlave;
    GameObject pistaLlave;

    void Start()
    {
        animacionPuertaVerja = verja.GetComponent<Animator>();
        recordarLlave = GameObject.Find("RecordatorioLlave");
        pistaLlave = GameObject.Find("ObservacionLlave");

        recordarLlave.SetActive(false);
        pistaLlave.SetActive(false);

        tieneLlave = false;
        llaveUsada = false;
        antiSpam = false;

        brillitoLlave.SetActive(false);
    }

    void OnTriggerStay(Collider jugador)
    {
        // Si se acerca Kaito a la puerta, al pulsar E interactuará con ella
        // Sin llave: Aviso buscar llave (si se hace 3 veces se le mostrará un brillo guía)
        // Con llave: Abre la puerta
        if (jugador.gameObject.name == "Kaito")
        {
            if (Input.GetKeyUp(KeyCode.E) && !llaveUsada && !antiSpam)
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
                    interacciones++;

                    StartCoroutine(Recordatorio());
                }
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

        if (interacciones < 3)
        {
            recordarLlave.SetActive(true);

            yield return new WaitForSeconds(3f);
            
            recordarLlave.SetActive(false);
        }
        else
        {
            pistaLlave.SetActive(true);

            yield return new WaitForSeconds(3f);

            pistaLlave.SetActive(false);
            
            brillitoLlave.SetActive(true);
        }

        antiSpam = false;
    }
}

