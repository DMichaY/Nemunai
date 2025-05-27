using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFightExtra : MonoBehaviour
{
    // Referencias
    public List<GameObject> HBs = new List<GameObject>();
    public List<string> HBNames = new List<string>();
    KaitoFight golpearKaito;

    private string lastActivatedHB;

    public AudioClip sonidoDEADKaitoO;

    public AudioClip sonidoATQKaitoO1;
    public AudioClip sonidoATQKaitoO2;
    public AudioClip sonidoATQKaitoO3;
    public AudioClip sonidoATQKaitoO4;

    public AudioClip sonidoMISSKaitoO1;
    public AudioClip sonidoMISSKaitoO2;
    public AudioClip sonidoMISSKaitoO3;
    public AudioClip sonidoMISSKaitoO4;

    public AudioClip sonidoHITKaitoO1;
    public AudioClip sonidoHITKaitoO2;
    public AudioClip sonidoHITKaitoO3;
    public AudioClip sonidoHITKaitoO4;
    
    AudioSource audioFuente;

    private List<AudioClip> listaSonidosATQKaitoO = new List<AudioClip>();
    private List<AudioClip> listaSonidosMISSKaitoO = new List<AudioClip>();
    private List<AudioClip> listaSonidosHITKaitoO = new List<AudioClip>();

    // Moverse

    public AudioClip correrTierra;
    public AudioClip correrPiedra;
    public AudioClip correrMadera;

    private string sueloActualTag = null;
    public bool enContactoConSuelo = false;

    public AudioSource audioFuentePisadas;

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
        listaSonidosATQKaitoO.Add(sonidoATQKaitoO1);
        listaSonidosATQKaitoO.Add(sonidoATQKaitoO2);
        listaSonidosATQKaitoO.Add(sonidoATQKaitoO3);
        listaSonidosATQKaitoO.Add(sonidoATQKaitoO4);

        listaSonidosMISSKaitoO.Add(sonidoMISSKaitoO1);
        listaSonidosMISSKaitoO.Add(sonidoMISSKaitoO2);
        listaSonidosMISSKaitoO.Add(sonidoMISSKaitoO3);
        listaSonidosMISSKaitoO.Add(sonidoMISSKaitoO4);

        listaSonidosHITKaitoO.Add(sonidoHITKaitoO1);
        listaSonidosHITKaitoO.Add(sonidoHITKaitoO2);
        listaSonidosHITKaitoO.Add(sonidoHITKaitoO3);
        listaSonidosHITKaitoO.Add(sonidoHITKaitoO4);

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
    public void SonidoATQKaitoOAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosATQKaitoO.Count);
        AudioClip sonidoSeleccionado = listaSonidosATQKaitoO[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    // Corrutina que produce sonido aleatorio del enemigo fallando golpe
    public void SonidoMISSKaitoOAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosMISSKaitoO.Count);
        AudioClip sonidoSeleccionado = listaSonidosMISSKaitoO[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    public void DetectarGolpe()
    {
        if (golpearKaito.isHit)
        {
            SonidoATQKaitoOAleatorio();
        }
        else
        {
            SonidoMISSKaitoOAleatorio();
        }
    }

    // Corrutina que produce sonido aleatorio del enemigo siendo golpeando
    public void SonidoHITKaitoOAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosHITKaitoO.Count);
        AudioClip sonidoSeleccionado = listaSonidosHITKaitoO[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    // Sonido inicio pelea
    public void SonidoDEADKaitoO()
    {
        audioFuente.PlayOneShot(sonidoDEADKaitoO);
    }

    // Sonidos Kaito Oscuro Moviendose Por Escena
    public void SonidosPisadas(bool enMovimiento)
    {
        // Si no está en contacto con el suelo o no hay audioFuente, detenemos cualquier sonido
        if (!enContactoConSuelo || audioFuentePisadas == null)
        {
            if (audioFuentePisadas != null)
                audioFuentePisadas.Stop();
            return;
        }

        AudioClip clipActual = null;

        if (sueloActualTag == "Tierra")
        {
            clipActual = correrTierra;
        }
        else if (sueloActualTag == "Piedra")
        {
            clipActual = correrPiedra;
        }
        else if (sueloActualTag == "Madera")
        {
            clipActual = correrMadera;
        }

        if (enMovimiento)
        {
            if (audioFuentePisadas.clip != clipActual)
            {
                audioFuentePisadas.clip = clipActual;
                audioFuentePisadas.pitch = 0.5f;
                audioFuentePisadas.volume = 40.0f;
                audioFuentePisadas.Play();
            }
            else if (!audioFuentePisadas.isPlaying)
            {
                audioFuentePisadas.pitch = 0.5f;
                audioFuentePisadas.volume = 40.0f;
                audioFuentePisadas.Play();
            }
        }
        else
        {
            audioFuentePisadas.Stop();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().CompareTag("Tierra") || collision.GetComponent<Collider>().CompareTag("Piedra") || collision.GetComponent<Collider>().CompareTag("Madera"))
        {
            sueloActualTag = collision.GetComponent<Collider>().tag;
            enContactoConSuelo = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.GetComponent<Collider>().tag == sueloActualTag)
        {
            sueloActualTag = null;
            enContactoConSuelo = false;
        }
    }
}