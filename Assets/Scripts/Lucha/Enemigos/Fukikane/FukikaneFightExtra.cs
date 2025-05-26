using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukikaneFightExtra : MonoBehaviour
{
    // Referencias
    public List<GameObject> HBs = new List<GameObject>();
    public List<string> HBNames = new List<string>();
    private ParticleSystem beam;
    KaitoFight golpearKaito;

    private string lastActivatedHB;

    public AudioClip sonidoATQFuk1;
    public AudioClip sonidoATQFuk2;
    public AudioClip sonidoATQFuk3;
    public AudioClip sonidoATQFukAlmas;
    public AudioClip sonidoATQFukSalto;

    public AudioClip sonidoMISSFuks1;
    public AudioClip sonidoMISSFuk2;
    public AudioClip sonidoMISSFuk3;

    public AudioClip sonidoHITFuk1;
    public AudioClip sonidoHITFuk2;
    public AudioClip sonidoHITFuk3;
    
    AudioSource audioFuente;

    private List<AudioClip> listaSonidosATQFuk = new List<AudioClip>();
    private List<AudioClip> listaSonidosMISSFuk = new List<AudioClip>();
    private List<AudioClip> listaSonidosHITFuk = new List<AudioClip>();

    private void Awake()
    {
        golpearKaito = FindObjectOfType<KaitoFight>();

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

        // Audio
        // Llenar la lista de sonidos
        listaSonidosATQFuk.Add(sonidoATQFuk1);
        listaSonidosATQFuk.Add(sonidoATQFuk2);
        listaSonidosATQFuk.Add(sonidoATQFuk3);
        listaSonidosMISSFuk.Add(sonidoMISSFuks1);
        listaSonidosMISSFuk.Add(sonidoMISSFuk2);
        listaSonidosMISSFuk.Add(sonidoMISSFuk3);
        listaSonidosHITFuk.Add(sonidoHITFuk1);
        listaSonidosHITFuk.Add(sonidoHITFuk2);
        listaSonidosHITFuk.Add(sonidoHITFuk3);

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

    // Corrutina que produce sonido aleatorio del enemigo atacando
    public void SonidoATQFukAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosATQFuk.Count);
        AudioClip sonidoSeleccionado = listaSonidosATQFuk[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    // Ataques especiales
    public void SonidoAtaqueSaltoFuk()
    {
        audioFuente.PlayOneShot(sonidoATQFukSalto);
    }

    public void SonidoAtaqueAlmasFuk()
    {
        audioFuente.PlayOneShot(sonidoATQFukAlmas);
    }

    // Corrutina que produce sonido aleatorio del enemigo fallando golpe
    public void SonidoMISSFukAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosMISSFuk.Count);
        AudioClip sonidoSeleccionado = listaSonidosMISSFuk[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }

    public void DetectarGolpe()
    {
        if (golpearKaito.isHit)
        {
            SonidoMISSFukAleatorio();
            SonidoATQFukAleatorio();
        }
        else
        {
            SonidoMISSFukAleatorio();
        }
    }

    // Corrutina que produce sonido aleatorio del enemigo siendo golpeando
    public void SonidoHITFukAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosHITFuk.Count);
        AudioClip sonidoSeleccionado = listaSonidosHITFuk[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }
}
