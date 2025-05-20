using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolpeInverso : MonoBehaviour
{

    public GameObject espiral1;

    public GameObject espiral2;

    public GameObject espiral3;

    public GameObject espiral4;





    public void RecibirGolpeInverso()
    {

        StartCoroutine(Golpe());

        IEnumerator Golpe()
        {

            espiral1.GetComponent<Image>().CrossFadeAlpha(0, 2, false);

            espiral1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 5f);


            espiral2.GetComponent<Image>().CrossFadeAlpha(0, 2, false);

            espiral2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 5f);


            espiral3.GetComponent<Image>().CrossFadeAlpha(0, 2, false);

            espiral3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 5f);


            espiral4.GetComponent<Image>().CrossFadeAlpha(0, 2, false);

            espiral4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 5f);


            yield return new WaitForSeconds(2f);

            espiral1.GetComponent<Image>().color = new Color(255f,255f,255f,0f);

            espiral2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);

            espiral3.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);

            espiral4.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);


            yield return new WaitForSeconds(0.2f);

            espiral1.GetComponent<Image>().CrossFadeAlpha(1, 2, false);

            espiral2.GetComponent<Image>().CrossFadeAlpha(1, 2, false);

            espiral3.GetComponent<Image>().CrossFadeAlpha(1, 2, false);

            espiral4.GetComponent<Image>().CrossFadeAlpha(1, 2, false);






        }
       
        




    }
    



}
