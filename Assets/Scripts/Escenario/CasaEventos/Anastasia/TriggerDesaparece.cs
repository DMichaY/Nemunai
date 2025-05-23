using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDesaparece : MonoBehaviour
{
    public GameObject anastasiaCocina;

  


    private void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {

            if (anastasiaCocina.activeSelf)
            {

                anastasiaCocina.SetActive(false);

            }



        }

    }
}
