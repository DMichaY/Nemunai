using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesactivarPelea : MonoBehaviour
{

    public GameObject pelea;

    public GameObject camPelea;

    public GameObject camPueblo;

    public GameObject jugadorPueblo;
    
    public Image pantallaNegra;




    public void Desactiva()
    {

        StartCoroutine(EmpezarPelea());


        IEnumerator EmpezarPelea()
        {

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


            camPueblo.SetActive(true);

            camPelea.SetActive(false);

            jugadorPueblo.SetActive(true);

            jugadorPueblo.GetComponent<KaitoMovimiento>().enabled = true;


            jugadorPueblo.GetComponent<KaitoMovimiento>().movimiento = new Vector3(0, 0, 0);


            pelea.SetActive(false);

        }

        

    }
    


   
}
