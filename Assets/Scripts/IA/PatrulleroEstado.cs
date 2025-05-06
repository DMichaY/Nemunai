using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrulleroEstado
{

    public PatrulleroIA patrulleroIA;

    public void inicializarVariables(PatrulleroIA _patrulleroIA)
    {
        patrulleroIA = _patrulleroIA;


    }

    public enum ESTADO
    {
        PATRULLEROVIGILAR, PATRULLEROATACAR
    };

    public enum EVENTO
    {
        ENTRAR, ACTUALIZAR, SALIR
    };

    public ESTADO nombre;
    public EVENTO faseActual; 
    public PatrulleroEstado siguienteEstado;

    public PatrulleroEstado()
    {
    }

    public virtual void Entrar() { faseActual = EVENTO.ACTUALIZAR; }
    public virtual void Actualizar() { faseActual = EVENTO.ACTUALIZAR; } 
    public virtual void Salir() { faseActual = EVENTO.SALIR; }


    public PatrulleroEstado Procesar()
    {
        if (faseActual == EVENTO.ENTRAR) Entrar();
        if (faseActual == EVENTO.ACTUALIZAR) Actualizar();
        if (faseActual == EVENTO.SALIR)
        {
            Salir();
            return siguienteEstado; 
        }
        return this; 
    }

    protected void Patrullando()
    {

        Vector3 pos1 = patrulleroIA.destino1.transform.position;

        Vector3 pos2 = patrulleroIA.destino2.transform.position;

        
        if (Vector3.Distance(patrulleroIA.gameObject.transform.position, pos1) <= 1f)
        {
            
            patrulleroIA.gameObject.GetComponent<NavMeshAgent>().SetDestination(pos2);
        }


        if (Vector3.Distance(patrulleroIA.gameObject.transform.position, pos2) <= 1f)
        {

            patrulleroIA.gameObject.GetComponent<NavMeshAgent>().SetDestination(pos1);
        }


    }


    protected bool PuedeVerJugador()
    {
       
        RaycastHit hit;

        LayerMask layerMask = LayerMask.GetMask("Kaito");

        patrulleroIA.ray.origin = patrulleroIA.transform.position;

        patrulleroIA.ray.direction = patrulleroIA.transform.forward;

        Debug.DrawRay(patrulleroIA.ray.origin, patrulleroIA.ray.direction * 15, Color.red);

        // mirar
        if (Physics.Raycast(patrulleroIA.ray, out hit, 1 << 7))
        {
            
            if (hit.collider.name == "Kaito")
            {
                return true;
            }

         
        }

        return false;

    }

    protected void Disparar()
    {
        

        Debug.Log("Disparando");
    }

       
  


}

