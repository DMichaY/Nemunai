using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukikaneFightExtra : MonoBehaviour
{
    // Referencias
    public List<GameObject> HBs = new List<GameObject>();
    public List<string> HBNames = new List<string>();
    private ParticleSystem beam;

    private string lastActivatedHB;

    private void Awake()
    {
        foreach (Collider HB in transform.GetComponentsInChildren<Collider>())
        {
            if (HB.tag == "HurtBox")
            {
                HBs.Add(HB.gameObject);
                HBNames.Add(HB.gameObject.name);
                HB.gameObject.SetActive(false);
            }
        }
        beam = GetComponentInChildren<ParticleSystem>();
        beam.Pause();
    }

    public void ActivateHurtbox(string hbName)
    {
        if (lastActivatedHB != hbName) HBs[HBNames.IndexOf(hbName)].GetComponent<AttackScript>().damage += 5;
        HBs[HBNames.IndexOf(hbName)].SetActive(true);
    }

    public void DeactivateHurtbox(string hbName)
    {
        HBs[HBNames.IndexOf(hbName)].SetActive(false);
        if (lastActivatedHB != hbName) HBs[HBNames.IndexOf(hbName)].GetComponent<AttackScript>().damage -= 5;
        GetComponent<Animator>().SetBool("isAttacking", false);
        lastActivatedHB = hbName;
    }

    public void ExplosionStart()
    {
        StartCoroutine("Explosion");
    }

    private IEnumerator Explosion()
    {
        float size = 0;
        HBs[HBNames.IndexOf("HB_Boom")].SetActive(true);
        while (size < 10)
        {
            size += Time.deltaTime * 30;
            HBs[HBNames.IndexOf("HB_Boom")].transform.localScale = new Vector3(1, 1, 1) * size;
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        HBs[HBNames.IndexOf("HB_Boom")].transform.localScale = Vector3.zero;
        HBs[HBNames.IndexOf("HB_Boom")].SetActive(false);
    }

    public void PlayBeam()
    {
        beam.Play();
    }

    public void StopBeam()
    {
        beam.Stop();
    }
}
