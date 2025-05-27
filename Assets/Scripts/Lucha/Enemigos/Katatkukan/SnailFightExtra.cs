using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailFightExtra : MonoBehaviour
{
    // Referencias
    public List<GameObject> HBs = new List<GameObject>();
    public List<string> HBNames = new List<string>();

    private string lastActivatedHB;

    public AudioClip sonidoDEADKat;
    public AudioClip sonidoJUMPKat;
    public AudioClip sonidoSPLASHKat;
    public AudioClip sonidoBLQKat;

    public AudioClip sonidoATQKat1;
    public AudioClip sonidoATQKat2;

    public AudioClip sonidoVOZKat1;
    public AudioClip sonidoVOZKat2;

    public AudioClip sonidoMISSKat1;
    public AudioClip sonidoMISSKat2;
    public AudioClip sonidoMISSKat3;

    public AudioClip sonidoHITKat1;
    public AudioClip sonidoHITKat2;
    public AudioClip sonidoHITKat3;
    
    AudioSource audioFuente;
    bool BLQReverso = false;

    private List<AudioClip> listaSonidosATQKat = new List<AudioClip>();
    private List<AudioClip> listaSonidosMISSKat = new List<AudioClip>();
    private List<AudioClip> listaSonidosHITKat = new List<AudioClip>();

    public AudioClip arrastrarseCaracol;

    public AudioSource audioFuenteArrastrarse;

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
        listaSonidosATQKat.Add(sonidoATQKat1);
        listaSonidosATQKat.Add(sonidoATQKat2);

        listaSonidosMISSKat.Add(sonidoMISSKat1);
        listaSonidosMISSKat.Add(sonidoMISSKat2);
        listaSonidosMISSKat.Add(sonidoMISSKat3);

        listaSonidosHITKat.Add(sonidoHITKat1);
        listaSonidosHITKat.Add(sonidoHITKat2);
        listaSonidosHITKat.Add(sonidoHITKat3);

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
        lastActivatedHB = hbName;
    }

    public void ClearHBs()
    {
        foreach (GameObject hb in HBs) hb.SetActive(false);
    }

    public void StartBlocking()
    {
        GetComponentInParent<SnailFightAI>().isBlocking = true;
    }

    public void AttackEnd()
    {
        GetComponent<Animator>().SetBool("isAttacking", false);
    }

    // Corrutina que produce sonido aleatorio del enemigo atacando
    public void SonidoATQKatAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosATQKat.Count);
        AudioClip sonidoSeleccionado = listaSonidosATQKat[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    // Corrutina que produce sonido aleatorio del enemigo fallando golpe
    public void SonidoMISSKatAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosMISSKat.Count);
        AudioClip sonidoSeleccionado = listaSonidosMISSKat[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    // Corrutina que produce sonido aleatorio del enemigo siendo golpeando
    public void SonidoHITKatAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosHITKat.Count);
        AudioClip sonidoSeleccionado = listaSonidosHITKat[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    // Sonidos voces ataque caracol
    public void SonidoVOZKat1()
    {
        audioFuente.PlayOneShot(sonidoVOZKat1);
    }

    public void SonidoVOZKat2()
    {
            audioFuente.PlayOneShot(sonidoVOZKat2);
            BLQReverso = true;
    }

    // Sonido salto caracol
    public void SonidoJUMPKat()
    {
        if (!BLQReverso)
        {
            audioFuente.PlayOneShot(sonidoJUMPKat);
            BLQReverso = true;
        }
    }

    public void SonidoJUMPKatReverso()
    {
        if (BLQReverso)
        {
            audioFuente.PlayOneShot(sonidoJUMPKat);
            BLQReverso = false;
        }
    }
    
    // Sonido caida caracol
    public void SonidoSPLASHKat()
    {
        if (!BLQReverso)
        {
            audioFuente.PlayOneShot(sonidoSPLASHKat);
            BLQReverso = true;
        }
    }

    public void SonidoSPLASHKatReverso()
    {
        if (BLQReverso)
        {
            audioFuente.PlayOneShot(sonidoSPLASHKat);
            BLQReverso = false;
        }
    }

    // Sonidos bloqueo caracol
    public void SonidoBLQKat()
    {
        if (!BLQReverso)
        {
            audioFuente.PlayOneShot(sonidoBLQKat);
            BLQReverso = true;
        }
    }

    public void SonidoBLQKatReverso()
    {
        if (BLQReverso)
        {
            audioFuente.PlayOneShot(sonidoBLQKat);
            BLQReverso = false;
        }
    }

    // Sonido muerte
    public void SonidoDEADKat()
    {
        audioFuente.PlayOneShot(sonidoDEADKat);
    }

    public void SonidoArrastrarse()
    {
        audioFuenteArrastrarse.PlayOneShot(arrastrarseCaracol);
    }
}