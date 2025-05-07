using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarPelea : MonoBehaviour
{

    public GameObject pelea;

    public GameObject camPelea;

    public GameObject camPueblo;

    public GameObject jugadorPueblo;

    


    public void Desactiva()
    {

        camPueblo.SetActive(true);

        camPelea.SetActive(false);

        jugadorPueblo.SetActive(true);


        jugadorPueblo.GetComponent<KaitoMovimiento>().movimiento = new Vector3(0,0,0);


        pelea.SetActive(false);

    }
}
