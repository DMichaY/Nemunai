using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivarPelea : MonoBehaviour
{

    public string tagJugador = "Player";

    public GameObject pelea;

    public GameObject camPelea;

    public GameObject camPueblo;

    public GameObject jugadorPueblo;

    public GameObject vecinoPueblo;

    public Image pantallaNegra;
    
    




    private void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag(tagJugador))
        {
            StartCoroutine(EmpezarPelea());


        }

        IEnumerator EmpezarPelea()
        {

            jugadorPueblo.GetComponent<KaitoMovimiento>().enabled = false;

            if (pantallaNegra != null)
            {
                pantallaNegra.CrossFadeAlpha(1, 2, false);

            }

            else
            {
                Debug.LogWarning("No hay pantalla negra!");
            }

            yield return new WaitForSeconds(2f);



            yield return new WaitForSeconds(0.2f);

            if (pantallaNegra != null)
            {
                pantallaNegra.CrossFadeAlpha(0, 2, false);

            }

            else
            {
                Debug.LogWarning("No hay pantalla negra!");
            }

            pelea.SetActive(true);

            camPueblo.SetActive(false);

            camPelea.SetActive(true);

            jugadorPueblo.SetActive(false);

            vecinoPueblo.SetActive(false);

        }
    }



   
}
