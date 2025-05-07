using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPelea : MonoBehaviour
{

    public string tagJugador = "Player";

    public GameObject pelea;

    public GameObject camPelea;

    public GameObject camPueblo;

    public GameObject jugadorPueblo;

    public GameObject vecinoPueblo;




    private void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag(tagJugador))
        {
            pelea.SetActive(true);

            camPueblo.SetActive(false);

            camPelea.SetActive(true);

            jugadorPueblo.SetActive(false);




            vecinoPueblo.SetActive(false);
        }
    }


   
}
