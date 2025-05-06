using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrulleroAtacar : PatrulleroEstado
{
    

    public PatrulleroAtacar() : base()
    {
        // Guardamos el nombre del estado en el que nos encontramos.


        Debug.Log("ATACAR");
        nombre = ESTADO.PATRULLEROATACAR;
    }

    public override void Entrar()
    {
        
        patrulleroIA.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        base.Entrar();
    }


    public override void Actualizar()
    {



        

        


        if (!PuedeVerJugador())
        {
            siguienteEstado = new PatrulleroVigilar();

            siguienteEstado.inicializarVariables(patrulleroIA);

            faseActual = EVENTO.SALIR;
        }

        else
        {
            Debug.Log("KAITO KAITO");
            patrulleroIA.gameObject.GetComponent<NavMeshAgent>().SetDestination(patrulleroIA.jugador.transform.position);

            
        }

        
        
        

    }

    public override void Salir()
    {
        
        base.Salir();
    }




}
