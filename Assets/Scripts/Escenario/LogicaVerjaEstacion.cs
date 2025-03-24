using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaVerjaEstacion : MonoBehaviour
{
    // Variables
    Animator animacionPuertaVerja;

    public bool tieneLlave;

    public GameObject imagenLlave;

    void Start()
    {
        animacionPuertaVerja = this.GetComponent<Animator>();
        tieneLlave = false;
    }

    void OnCollisionEnter(Collision jugador)
    {
        if (jugador.gameObject.name == "Kaito")
        {
            if (tieneLlave)
            {
                GetComponent<Renderer>().material.color = Color.green;
                animacionPuertaVerja.SetBool("usaLlave", true);

                imagenLlave.SetActive(false);
            }
        }
    }

}
