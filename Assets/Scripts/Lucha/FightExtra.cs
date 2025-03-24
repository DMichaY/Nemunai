using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightExtra : MonoBehaviour
{
    private KaitoFight kaitoScript;

    private List<GameObject> HBs = new List<GameObject>();
    public List<string> HBNames = new List<string>();

    private void Awake()
    {
        kaitoScript= FindObjectOfType<KaitoFight>();

        foreach (Collider HB in transform.GetComponentsInChildren<Collider>())
        {
            if(HB.tag == "HurtBox")
            {
            HBs.Add(HB.gameObject);
            HBNames.Add(HB.gameObject.name);
            HB.gameObject.SetActive(false);
            }
        }
    }

    public void ActivateMovement()
    {
        kaitoScript.ActivateMovement();
    }

    public void DeactivateMovement()
    {
        print("PRUEBA");
        kaitoScript.DeactivateMovement();
    }

    public void ActivateHurtbox (string hbName)
    {
        HBs[HBNames.IndexOf(hbName)].SetActive(true);
    }

    public void DeactivateHurtbox(string hbName)
    {
        HBs[HBNames.IndexOf(hbName)].SetActive(false);
    }
}