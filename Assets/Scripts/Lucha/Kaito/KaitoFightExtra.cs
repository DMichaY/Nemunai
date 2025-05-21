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

    public AudioClip sonidoATQPos1;
    public AudioClip sonidoATQPos2;
    public AudioClip sonidoATQPos3;
    public AudioClip sonidoATQPos4;

    public AudioClip sonidoMISSPos1;
    public AudioClip sonidoMISSPos2;
    public AudioClip sonidoMISSPos3;
    public AudioClip sonidoMISSPos4;
    
    AudioSource audioFuente;

    private List<AudioClip> listaSonidosATQPos = new List<AudioClip>();
    private List<AudioClip> listaSonidosMISSPos = new List<AudioClip>();

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
        listaSonidosATQPos.Add(sonidoATQPos1);
        listaSonidosATQPos.Add(sonidoATQPos2);
        listaSonidosATQPos.Add(sonidoATQPos3);
        listaSonidosATQPos.Add(sonidoATQPos4);

        listaSonidosMISSPos.Add(sonidoMISSPos1);
        listaSonidosMISSPos.Add(sonidoMISSPos2);
        listaSonidosMISSPos.Add(sonidoMISSPos3);
        listaSonidosMISSPos.Add(sonidoMISSPos4);

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

    // Corrutina que produce sonido aleatorio del enemigo atacando/fallando ataque
    public void SonidoATQKaitoAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosATQPos.Count);
        AudioClip sonidoSeleccionado = listaSonidosATQPos[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    // Corrutina que produce sonido aleatorio del enemigo fallando golpe
    public void SonidoMISSKaitoAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosMISSPos.Count);
        AudioClip sonidoSeleccionado = listaSonidosMISSPos[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }
}