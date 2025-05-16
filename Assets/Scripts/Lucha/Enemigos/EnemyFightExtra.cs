using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightExtra : MonoBehaviour
{
    // Referencias
    public List<GameObject> HBs = new List<GameObject>();
    public List<string> HBNames = new List<string>();

    private string lastActivatedHB;

    public AudioClip sonidoATQPos1;
    public AudioClip sonidoATQPos2;
    public AudioClip sonidoATQPos3;
    public AudioClip sonidoATQPos4;
    public AudioClip sonidoATQPos5;
    public AudioClip sonidoATQPos6;
    public AudioClip sonidoATQPos7;
    public AudioClip sonidoATQPos8;
    AudioSource audioFuente;

    private List<AudioClip> listaSonidosPos = new List<AudioClip>();

    private void Awake()
    {
        foreach (Collider HB in transform.GetComponentsInChildren<Collider>())
        {
            if(HB.tag == "HurtBox")
            {
                HBs.Add(HB.gameObject);
                HBNames.Add(HB.gameObject.name);
                HB.gameObject.SetActive(false);
            }
        }

        // Audio
        // Llenar la lista de sonidos
        listaSonidosPos.Add(sonidoATQPos1);
        listaSonidosPos.Add(sonidoATQPos2);
        listaSonidosPos.Add(sonidoATQPos3);
        listaSonidosPos.Add(sonidoATQPos4);
        listaSonidosPos.Add(sonidoATQPos5);
        listaSonidosPos.Add(sonidoATQPos6);
        listaSonidosPos.Add(sonidoATQPos7);
        listaSonidosPos.Add(sonidoATQPos8);

        audioFuente = this.GetComponent<AudioSource>();
    }

    public void ActivateHurtbox (string hbName)
    {
        if (lastActivatedHB != hbName) HBs[HBNames.IndexOf(hbName)].GetComponent<AttackScript>().damage += 5;
        HBs[HBNames.IndexOf(hbName)].SetActive(true);
        SonidoATQPosAleatorio();
    }

    public void DeactivateHurtbox(string hbName)
    {
        HBs[HBNames.IndexOf(hbName)].SetActive(false);
        if (lastActivatedHB != hbName) HBs[HBNames.IndexOf(hbName)].GetComponent<AttackScript>().damage -= 5;
        GetComponent<Animator>().SetBool("isAttacking", false);
        lastActivatedHB = hbName;
    }

    // Corrutina que produce sonido aleatorio del enemigo atacando
    public void SonidoATQPosAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosPos.Count);
        AudioClip sonidoSeleccionado = listaSonidosPos[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }
}