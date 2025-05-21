using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditosIniciales : MonoBehaviour
{

    public GameObject texto;


    public void Start()
    {
        
            StartCoroutine(EmpezarPelea());


        
        IEnumerator EmpezarPelea()
        {

            texto.GetComponent<TextMeshProUGUI>().color = new Color(255f, 255f, 255f, 5f);
           

            
            yield return new WaitForSeconds(2f);


            texto.GetComponent<TextMeshProUGUI>().color = new Color(255f, 255f, 255f, 0f);







        }
    }
}
