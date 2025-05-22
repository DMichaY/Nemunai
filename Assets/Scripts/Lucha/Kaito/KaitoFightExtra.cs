using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class KaitoFightExtra : MonoBehaviour
{
    private KaitoFight kaitoScript;
    EnemyFightAI sonidosEnemigoDemPos2;

    public List<GameObject> HBs = new List<GameObject>();
    public List<string> HBNames = new List<string>();

    public AudioClip sonidoDEADKaito;

    public AudioClip sonidoATQKaito1;
    public AudioClip sonidoATQKaito2;
    public AudioClip sonidoATQKaito3;
    public AudioClip sonidoATQKaito4;

    public AudioClip sonidoMISSKaito1;
    public AudioClip sonidoMISSKaito2;
    public AudioClip sonidoMISSKaito3;
    public AudioClip sonidoMISSKaito4;

    public AudioClip sonidoHITKaito1;
    public AudioClip sonidoHITKaito2;
    public AudioClip sonidoHITKaito3;
    public AudioClip sonidoHITKaito4;
    
    AudioSource audioFuente;

    private List<AudioClip> listaSonidosATQKaito = new List<AudioClip>();
    private List<AudioClip> listaSonidosMISSKaito = new List<AudioClip>();
    private List<AudioClip> listaSonidosHITKaito = new List<AudioClip>();

    private string lastActivatedHB;

    private void Awake()
    {
        kaitoScript= FindObjectOfType<KaitoFight>();
        sonidosEnemigoDemPos2 = FindObjectOfType<EnemyFightAI>();

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
        listaSonidosATQKaito.Add(sonidoATQKaito1);
        listaSonidosATQKaito.Add(sonidoATQKaito2);
        listaSonidosATQKaito.Add(sonidoATQKaito3);
        listaSonidosATQKaito.Add(sonidoATQKaito4);

        listaSonidosMISSKaito.Add(sonidoMISSKaito1);
        listaSonidosMISSKaito.Add(sonidoMISSKaito2);
        listaSonidosMISSKaito.Add(sonidoMISSKaito3);
        listaSonidosMISSKaito.Add(sonidoMISSKaito4);

        listaSonidosHITKaito.Add(sonidoHITKaito1);
        listaSonidosHITKaito.Add(sonidoHITKaito2);
        listaSonidosHITKaito.Add(sonidoHITKaito3);
        listaSonidosHITKaito.Add(sonidoHITKaito4);

        audioFuente = this.GetComponent<AudioSource>();
    }

    public void ActivateMovement()
    {
        kaitoScript.ActivateMovement();
    }

    public void DeactivateMovement()
    {
        kaitoScript.DeactivateMovement();
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

    // Produce sonido aleatorio de Kaito atacando/fallando ataque
    public void SonidoATQKaitoAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosATQKaito.Count);
        AudioClip sonidoSeleccionado = listaSonidosATQKaito[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    // Produce sonido aleatorio de Kaito fallando golpe
    public void SonidoMISSKaitoAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosMISSKaito.Count);
        AudioClip sonidoSeleccionado = listaSonidosMISSKaito[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    // Produce sonido  aleatorio de Kaito siendo herido
    public void SonidoHITKaitoAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosHITKaito.Count);
        AudioClip sonidoSeleccionado = listaSonidosHITKaito[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    public void SonidoDEADKaito()
    {
        audioFuente.PlayOneShot(sonidoDEADKaito);
    }
}