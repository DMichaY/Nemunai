using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaVerjaEstacion : MonoBehaviour
{
    // Variables
    Animator animacionPuertaVerja;
    public GameObject llaveVerja;

    public int interacciones = 0;

    public bool tieneLlave;
    public bool interaccionUno;

    public GameObject imagenLlave;
    GameObject recordarLlave;

    void Start()
    {
        animacionPuertaVerja = this.GetComponent<Animator>();
        recordarLlave = GameObject.Find("RecordatorioLlave");

        recordarLlave.SetActive(false);

        tieneLlave = false;
        interaccionUno = true;
    }

    void OnCollisionStay(Collision jugador)
    {
        // Si se acerca Kaito a la puerta, al pulsar E interactuará con ella
        // Sin llave: Aviso buscar llave (si se hace 3 veces se le mostrará un brillo guía)
        // Con llave: Abre la puerta
        if (jugador.gameObject.name == "Kaito")
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (tieneLlave)
                {
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
                        interacciones++;
                    }
                    StartCoroutine(Recordatorio());
                }
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

