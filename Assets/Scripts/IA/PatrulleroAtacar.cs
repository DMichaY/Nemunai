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


        //velocidad del zombie
        patrulleroIA.gameObject.GetComponent<NavMeshAgent>().speed = 1.5f;

        //velocidad de la animacion
        patrulleroIA.gameObject.GetComponent<Animator>().speed = 3f;


        base.Entrar();
    }


    public override void Actualizar()
    {

        patrulleroIA.gameObject.GetComponent<NavMeshAgent>().SetDestination(patrulleroIA.jugador.transform.position);
    
        patrulleroIA.timer += Time.deltaTime;
    
        
        if (!PuedeVerJugador() && patrulleroIA.timer > 5f)
        {

            patrulleroIA.timer = 0;

            //velocidad del zombie
            patrulleroIA.gameObject.GetComponent<NavMeshAgent>().speed = 0.5f;

            //velocidad de la animacion
            patrulleroIA.gameObject.GetComponent<Animator>().speed = 1f;


            siguienteEstado = new PatrulleroVigilar();

            siguienteEstado.inicializarVariables(patrulleroIA);

            faseActual = EVENTO.SALIR;
        }

        
    

    }

    public override void Salir()
    {
        
        base.Salir();
    }




}
