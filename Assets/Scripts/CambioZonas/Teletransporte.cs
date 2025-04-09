using UnityEngine;
using UnityEngine.UI;


public class Teletransporte : MonoBehaviour
{
    public Transform destino;
    public string tagJugador = "Player";

    public Image pantallaNegra;

    private void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag(tagJugador))
        {
            if (destino != null)
            {

                if (pantallaNegra != null)
                {
                    pantallaNegra.CrossFadeAlpha(1, 2, false);

                }

                else
                {
                    Debug.LogWarning("No hay pantalla negra!");
                }
                
                

                otro.transform.position = destino.position;
                otro.transform.rotation = destino.rotation;

                
                if (pantallaNegra != null)
                {
                    pantallaNegra.CrossFadeAlpha(0, 2, false);

                }

                else
                {
                    Debug.LogWarning("No hay pantalla negra!");
                }
            }
        }
    }



}
