using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrulleroIA : MonoBehaviour
{

    PatrulleroEstado FSM;

    public GameObject jugador;

    public GameObject destino1;

    public GameObject destino2;

    public Ray ray;



    void Start()
    {
        
        FSM = new PatrulleroVigilar(); 

        FSM.inicializarVariables(this);

        
    }

    void Update()
    {
        FSM = FSM.Procesar(); 
    }

   
}
