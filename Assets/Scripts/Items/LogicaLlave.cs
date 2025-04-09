using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaLlave : MonoBehaviour
{
    // Variables
    public LogicaVerjaEstacion puertaEstacion;

    public GameObject imagenLlave;
    public GameObject brillitoLlave;
    GameObject llaveObtenidaUI;

    bool antiSpam;

    void Start()
    {
        puertaEstacion = puertaEstacion.GetComponent<LogicaVerjaEstacion>();
        llaveObtenidaUI = GameObject.Find("LlaveObtenida");

        antiSpam = false;

        gameObject.SetActive(false);
        llaveObtenidaUI.SetActive(false);
    }

    void OnTriggerStay(Collider jugador)
    {
        // Al entrar en contacto con el trigger, si se pulsa E se obtendrá la llave, avisandose por la UI
        if (jugador.gameObject.name == "Kaito")
        {
            if (Input.GetKeyUp(KeyCode.E) && !antiSpam)
            {
                puertaEstacion.tieneLlave = true;

                imagenLlave.SetActive(true);
                StartCoroutine(LlaveObtenida());
                StartCoroutine(noSpamInteractuar());
            }
        }
    }

    // Muestra el mensaje de llave obtenida durante 3 segundos
    IEnumerator LlaveObtenida()
    {
        llaveObtenidaUI.SetActive(true);
        Destroy(brillitoLlave);

        yield return new WaitForSeconds(3f);
        
        llaveObtenidaUI.SetActive(false);
        Destroy(this.gameObject);
    }

    IEnumerator noSpamInteractuar()
    {
        antiSpam = true;

        yield return new WaitForSeconds (3f);

        antiSpam = false;
    }
}
