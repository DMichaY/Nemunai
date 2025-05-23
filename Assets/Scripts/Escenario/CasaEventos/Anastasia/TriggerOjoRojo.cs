using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOjoRojo : MonoBehaviour
{

    public GameObject anastasiaBrasero;

    public Material sangre;


    private void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {

            if (anastasiaBrasero.activeInHierarchy)
            {

                anastasiaBrasero.GetComponent<SkinnedMeshRenderer>().material = sangre;

            }



        }

    }




}
