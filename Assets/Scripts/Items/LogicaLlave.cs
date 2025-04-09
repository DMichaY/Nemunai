using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaLlave : MonoBehaviour
{
    // Variables
    public LogicaVerjaEstacion puertaEstacion;

    public GameObject imagenLlave;
    GameObject llaveObtenidaUI;

    void Start()
    {
        puertaEstacion = puertaEstacion.GetComponent<LogicaVerjaEstacion>();
        llaveObtenidaUI = GameObject.Find("LlaveObtenida");

        gameObject.SetActive(false);
        llaveObtenidaUI.SetActive(false);
    }

    void OnTriggerStay(Collider jugador)
    {
        // Al entrar en contacto con el trigger, si se pulsa E se obtendrá la llave, avisandose por la UI
        if (jugador.gameObject.name == "Kaito")
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                Debug.Log("AY");
                puertaEstacion.tieneLlave = true;

                imagenLlave.SetActive(true);
                StartCoroutine(LlaveObtenida());
            }
        }
    }

    // Muestra el mensaje de llave obtenida durante 3 segundos
    IEnumerator LlaveObtenida()
    {
        llaveObtenidaUI.SetActive(true);

        yield return new WaitForSeconds(3f);
        
        llaveObtenidaUI.SetActive(false);
        Destroy(this.gameObject);
    }
}
