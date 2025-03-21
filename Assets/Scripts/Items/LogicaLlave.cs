using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaLlave : MonoBehaviour
{
    // Variables
    bool tieneLlave;

    void Start()
    {
        tieneLlave = false;
    }

    void OnTriggerEnter(Collider jugador)
    {
        if (jugador.gameObject.name == "Kaito")
        {
            tieneLlave = true;
            Destroy(this.gameObject);
        }
    }
}
