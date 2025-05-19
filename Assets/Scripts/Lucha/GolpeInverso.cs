using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolpeInverso : MonoBehaviour
{

    public GameObject espiral;



  
    public void RecibirGolpeInverso()
    {

        StartCoroutine(Golpe());

        IEnumerator Golpe()
        {
            
            if (espiral != null)
            {
                espiral.GetComponent<Image>().CrossFadeAlpha(0, 2, false);

                espiral.GetComponent<Image>().color = new Color(255f, 255f, 255f, 5f);

            }

            else
            {
                Debug.LogWarning("No hay pantalla negra!");
            }

            yield return new WaitForSeconds(2f);

            espiral.GetComponent<Image>().color = new Color(255f,255f,255f,0f);


            yield return new WaitForSeconds(0.2f);

            if (espiral != null)
            {
                espiral.GetComponent<Image>().CrossFadeAlpha(1, 2, false);

            }

            else
            {
                Debug.LogWarning("No hay pantalla negra!");
            }

            

        }
       
        




    }
    



}
