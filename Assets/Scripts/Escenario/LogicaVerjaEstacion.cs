using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Añadido para usar TextMeshPro

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

    // Texto
    GameObject recordarLlave;
    public TextMeshProUGUI recordarLlaveTMP; // Añadido
    private TypewriterEffect typewriter;     // Añadido

    void Start()
    {
        animacionPuertaVerja = verja.GetComponent<Animator>();
        recordarLlave = GameObject.Find("RecordatorioLlave");

        recordarLlave.SetActive(false);

        tieneLlave = false;
        llaveUsada = false;
        antiSpam = false;

        brillitoLlave.SetActive(false);

        typewriter = gameObject.AddComponent<TypewriterEffect>(); // Añadido
        recordarLlaveTMP.gameObject.SetActive(false);             // Añadido
    }

    public override void Interact()
    {
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

        typewriter.MostrarTexto(recordarLlaveTMP); // Reemplaza el activar/desactivar

        yield return new WaitForSeconds(5f); // Ajustado ligeramente para que dé tiempo al texto

        brillitoLlave.SetActive(true);

        antiSpam = false;
    }
}
