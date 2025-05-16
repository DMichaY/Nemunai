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

    public int contador = 0;


    void Start()
    {
        jugador = GameObject.FindWithTag("Player");


        FSM = new PatrulleroVigilar(); 

        FSM.inicializarVariables(this);

        
    }

    void Update()
    {
        FSM = FSM.Procesar(); 
    }

   
}
