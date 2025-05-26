using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightExtra : MonoBehaviour
{
    // Referencias
    public List<GameObject> HBs = new List<GameObject>();
    public List<string> HBNames = new List<string>();
    KaitoFight golpearKaito;

    private string lastActivatedHB;

    public AudioClip sonidoTAUNTPos;
    public AudioClip sonidoDEADPos;

    public AudioClip sonidoATQPos1;
    public AudioClip sonidoATQPos2;
    public AudioClip sonidoATQPos3;
    public AudioClip sonidoATQPos4;

    public AudioClip sonidoMISSPos1;
    public AudioClip sonidoMISSPos2;
    public AudioClip sonidoMISSPos3;
    public AudioClip sonidoMISSPos4;

    public AudioClip sonidoHITPos1;
    public AudioClip sonidoHITPos2;
    public AudioClip sonidoHITPos3;
    public AudioClip sonidoHITPos4;
    
    AudioSource audioFuente;

    private List<AudioClip> listaSonidosATQPos = new List<AudioClip>();
    private List<AudioClip> listaSonidosMISSPos = new List<AudioClip>();
    private List<AudioClip> listaSonidosHITPos = new List<AudioClip>();

    private void Awake()
    {
        golpearKaito = FindObjectOfType<KaitoFight>();

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
        listaSonidosATQPos.Add(sonidoATQPos1);
        listaSonidosATQPos.Add(sonidoATQPos2);
        listaSonidosATQPos.Add(sonidoATQPos3);
        listaSonidosATQPos.Add(sonidoATQPos4);

        listaSonidosMISSPos.Add(sonidoMISSPos1);
        listaSonidosMISSPos.Add(sonidoMISSPos2);
        listaSonidosMISSPos.Add(sonidoMISSPos3);
        listaSonidosMISSPos.Add(sonidoMISSPos4);

        listaSonidosHITPos.Add(sonidoHITPos1);
        listaSonidosHITPos.Add(sonidoHITPos2);
        listaSonidosHITPos.Add(sonidoHITPos3);
        listaSonidosHITPos.Add(sonidoHITPos4);

        audioFuente = this.GetComponent<AudioSource>();
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

    public void ClearHBs()
    {
        foreach (GameObject hb in HBs) hb.SetActive(false);
    }

    // Corrutina que produce sonido aleatorio del enemigo atacando
    public void SonidoATQPosAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosATQPos.Count);
        AudioClip sonidoSeleccionado = listaSonidosATQPos[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    // Corrutina que produce sonido aleatorio del enemigo fallando golpe
    public void SonidoMISSPosAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosMISSPos.Count);
        AudioClip sonidoSeleccionado = listaSonidosMISSPos[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    public void DetectarGolpe()
    {
        if (golpearKaito.isHit)
        {
            SonidoATQPosAleatorio();
        }
        else
        {
            SonidoMISSPosAleatorio();
        }
    }

    // Corrutina que produce sonido aleatorio del enemigo siendo golpeando
    public void SonidoHITPosAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosHITPos.Count);
        AudioClip sonidoSeleccionado = listaSonidosHITPos[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }
    
    // Sonido inicio pelea
    public void SonidoTAUNTPos()
    {
        audioFuente.PlayOneShot(sonidoTAUNTPos);
    }

    // Sonido inicio pelea
    public void SonidoDEADPos()
    {
        audioFuente.PlayOneShot(sonidoDEADPos);
    }
}