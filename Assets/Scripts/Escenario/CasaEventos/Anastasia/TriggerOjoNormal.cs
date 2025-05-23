using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOjoNormal : MonoBehaviour
{
    public GameObject anastasiaBrasero;

    public Material ojo;


    private void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {

            if (anastasiaBrasero.activeInHierarchy)
            {

                anastasiaBrasero.GetComponent<SkinnedMeshRenderer>().material = ojo;

            }



        }

    }

}
