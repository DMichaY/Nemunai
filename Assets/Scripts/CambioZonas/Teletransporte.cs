using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Teletransporte : MonoBehaviour
{
    public Transform destino;
    public string tagJugador = "Player";

    public Image pantallaNegra;

    public GameObject jugador;

    private void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag(tagJugador))
        {
            if (destino != null)
            {


                StartCoroutine(CambioPiso());


            }
        }

        
        IEnumerator CambioPiso()
        {

            jugador.GetComponent<KaitoMovimiento>().enabled = false;
            
            if (pantallaNegra != null)
            {
                pantallaNegra.CrossFadeAlpha(1, 2, false);

            }

            else
            {
                Debug.LogWarning("No hay pantalla negra!");
            }

            yield return new WaitForSeconds(2f);

            otro.transform.position = destino.position;
            otro.transform.rotation = destino.rotation;

            yield return new WaitForSeconds(0.2f);

            if (pantallaNegra != null)
            {
                pantallaNegra.CrossFadeAlpha(0, 2, false);

            }

            else
            {
                Debug.LogWarning("No hay pantalla negra!");
            }

            jugador.GetComponent<KaitoMovimiento>().enabled = true;

        }

        
    }



}
