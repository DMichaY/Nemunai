using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaLlave : Interactable
{
    // Variables
    Animator animacionAlfombra;
    public GameObject alfombra;
    public LogicaVerjaEstacion puertaEstacion;

    public GameObject imagenLlave;
    public GameObject brillitoLlave;
    GameObject llaveObtenidaUI;

    int numInter = 0;
    bool antiSpam;

    void Start()
    {
        animacionAlfombra = alfombra.GetComponent<Animator>();
        puertaEstacion = puertaEstacion.GetComponent<LogicaVerjaEstacion>();
        llaveObtenidaUI = GameObject.Find("LlaveObtenida");

        antiSpam = false;

        gameObject.SetActive(false);
        llaveObtenidaUI.SetActive(false);
    }

    public override void Interact()
    {
        // Al entrar en contacto con el trigger, si se pulsa E se obtendrá la llave, avisandose por la UI
        if (numInter == 0 && !antiSpam)
        {
            StartCoroutine(MoviendoAlfombra());
            numInter += 1;
        }
        else if (!antiSpam)
        {
            puertaEstacion.tieneLlave = true;

            imagenLlave.SetActive(true);
            StartCoroutine(LlaveObtenida());
        }
    }

    // Mueve la alfombra antes de cojer la llave
    IEnumerator MoviendoAlfombra()
    {
        antiSpam = true;
        animacionAlfombra.SetBool("MoverAlfombra", true);

        yield return new WaitForSeconds(2f);
        antiSpam = false;
    }

    // Muestra el mensaje de llave obtenida durante 3 segundos
    IEnumerator LlaveObtenida()
    {
        antiSpam = true;

        llaveObtenidaUI.SetActive(true);
        Destroy(brillitoLlave);

        yield return new WaitForSeconds(5f);
        
        llaveObtenidaUI.SetActive(false);
        antiSpam = false;
        Destroy(this.gameObject);
    }
}
