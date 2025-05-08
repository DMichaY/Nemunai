using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float damage = 5;
    private static GameObject hitEffect;

    private void Start()
    {
        if (hitEffect == null) hitEffect = GameObject.Find("HitEffect");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fighter")
        {
            other.GetComponent<FighterClass>().GetHit(damage);
            Destroy(Instantiate(hitEffect, other.ClosestPoint(transform.position) + new Vector3(2, 0, 0), Quaternion.identity), 1);
        }
    }
}
