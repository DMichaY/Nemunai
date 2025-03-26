using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaLlave : MonoBehaviour
{
    // Variables
    public LogicaVerjaEstacion puertaEstacion;

    public GameObject imagenLlave;

    void Start()
    {
        puertaEstacion = puertaEstacion.GetComponent<LogicaVerjaEstacion>();
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider jugador)
    {
        if (jugador.gameObject.name == "Kaito")
        {
            puertaEstacion.tieneLlave = true;

            imagenLlave.SetActive(true);

            Destroy(this.gameObject);
        }
    }
}
