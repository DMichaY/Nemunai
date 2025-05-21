using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float damage = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fighter")
        {
            other.GetComponent<FighterClass>().GetHit(damage, other.ClosestPoint(transform.position) + new Vector3(2, 0, 0));
        }
    }
}
