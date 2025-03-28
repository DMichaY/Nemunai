using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightExtra : MonoBehaviour
{
    private EnemyFightAI aiScript;

    private List<GameObject> HBs = new List<GameObject>();
    public List<string> HBNames = new List<string>();

    private string lastActivatedHB;

    private void Awake()
    {
        aiScript= FindObjectOfType<EnemyFightAI>();

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

    public void ActivateHurtbox (string hbName)
    {
        if (lastActivatedHB != hbName) HBs[HBNames.IndexOf(hbName)].GetComponent<AttackScript>().damage += 5;
        HBs[HBNames.IndexOf(hbName)].SetActive(true);
    }

    public void DeactivateHurtbox(string hbName)
    {
        HBs[HBNames.IndexOf(hbName)].SetActive(false);
        if (lastActivatedHB != hbName) HBs[HBNames.IndexOf(hbName)].GetComponent<AttackScript>().damage -= 5;
        lastActivatedHB = hbName;
    }
}