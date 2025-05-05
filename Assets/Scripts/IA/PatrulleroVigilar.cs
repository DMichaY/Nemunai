using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrulleroVigilar : PatrulleroEstado
{
    public PatrulleroVigilar() : base()
    {
        // Guardamos el nombre del estado en el que nos encontramos.
        Debug.Log("VIGILANDO");
        nombre = ESTADO.PATRULLEROVIGILAR;
    }

    public override void Entrar()
    {
        patrulleroIA.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        patrulleroIA.gameObject.GetComponent<NavMeshAgent>().SetDestination(patrulleroIA.destino2.transform.position);
        base.Entrar();
    }

    public override void Actualizar()
    {

        if (PuedeVerJugador())
        {
            siguienteEstado = new PatrulleroAtacar();

            siguienteEstado.inicializarVariables(patrulleroIA);

            faseActual = EVENTO.SALIR;

        }

        else
        {
            Patrullando();
        }
    }

    public override void Salir()
    {
       
        base.Salir();
    }
}
