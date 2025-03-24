using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fighter") other.GetComponent<FighterClass>().GetHit(5f);
        print("GOLPE");
    }
}
