using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrulleroAtacar : PatrulleroEstado
{
    //float totalSegundos;

    public PatrulleroAtacar() : base()
    {
        // Guardamos el nombre del estado en el que nos encontramos.


        Debug.Log("ATACAR");
        nombre = ESTADO.PATRULLEROATACAR;
    }

    public override void Entrar()
    {
        //totalSegundos = 2;
        patrulleroIA.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        base.Entrar();
    }


    public override void Actualizar()
    {
        /* totalSegundos = totalSegundos - Time.deltaTime;

         if (totalSegundos <= 0)
         {

             Disparar();


             totalSegundos = 2;


         }
  */
        Disparar();


        if (!PuedeVerJugador())
        {
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
