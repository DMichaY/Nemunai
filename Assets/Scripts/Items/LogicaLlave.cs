using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaLlave : MonoBehaviour
{
    // Variables
    public LogicaVerjaEstacion puertaEstacion;

    void Start()
    {
        puertaEstacion = puertaEstacion.GetComponent<LogicaVerjaEstacion>();
    }

    void OnTriggerEnter(Collider jugador)
    {
        if (jugador.gameObject.name == "Kaito")
        {
            puertaEstacion.tieneLlave = true;
            Destroy(this.gameObject);
        }
    }
}
