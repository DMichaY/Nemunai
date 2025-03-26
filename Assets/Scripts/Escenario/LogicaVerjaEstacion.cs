using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaVerjaEstacion : MonoBehaviour
{
    // Variables
    Animator animacionPuertaVerja;
    public GameObject llaveVerja;

    public bool tieneLlave;
    public bool interaccionUno;

    public GameObject imagenLlave;
    public GameObject recordarLlave;

    void Start()
    {
        animacionPuertaVerja = this.GetComponent<Animator>();
        recordarLlave = GameObject.Find("RecordatorioLlave");

        recordarLlave.SetActive(false);

        tieneLlave = false;
        interaccionUno = true;
    }

    void OnCollisionEnter(Collision jugador)
    {
        if (jugador.gameObject.name == "Kaito")
        {
            if (tieneLlave)
            {
                GetComponent<Renderer>().material.color = Color.green;
                animacionPuertaVerja.SetBool("usaLlave", true);
                StartCoroutine(BorrarPuerta());

                imagenLlave.SetActive(false);
            }
            else
            {
                if (interaccionUno)
                {
                    llaveVerja.SetActive(true);
                    interaccionUno = false;
                }
                StartCoroutine(Recordatorio());
            }
        }
    }

    IEnumerator BorrarPuerta()
    {
        yield return new WaitForSeconds(3f);

        Destroy(this.gameObject);
    }

    IEnumerator Recordatorio()
    {
        recordarLlave.SetActive(true);

        yield return new WaitForSeconds(3f);
        
        recordarLlave.SetActive(false);
    }
}
