using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FukikaneAI : FighterClass
{
    //Referencias
    public Slider lifeBar;
    public KaitoFight player;
    public FukikaneState FSM;
    public Animator animator;
    public Rigidbody rb;
    public GameObject hitEffect;

    public AudioClip sonidoHITPos1;
    public AudioClip sonidoHITPos2;
    public AudioClip sonidoHITPos3;
    public AudioClip sonidoHITPos4;
    public AudioClip sonidoHITPos5;
    public AudioClip sonidoHITPos6;
    public AudioClip sonidoHITPos7;
    public AudioClip sonidoHITPos8;
    AudioSource audioFuente;

    public GameObject cuerpoRefAudio;

    private List<AudioClip> listaSonidosPos = new List<AudioClip>();

    //Estadísticas
    public float speed = 6, waitTime = 3, attackTime = 1, life = 100, startWaitTime = 0;

    void Start()
    {
        player = FindObjectOfType<KaitoFight>();
        animator = GetComponentInChildren<Animator>();
        animator.StartPlayback();
        rb = GetComponent<Rigidbody>();

        lifeBar.maxValue = life;
        lifeBar.value = life;

        StartCoroutine(StartWait());

        // Audio
        // Llenar la lista de sonidos
        listaSonidosPos.Add(sonidoHITPos1);
        listaSonidosPos.Add(sonidoHITPos2);
        listaSonidosPos.Add(sonidoHITPos3);
        listaSonidosPos.Add(sonidoHITPos4);
        listaSonidosPos.Add(sonidoHITPos5);
        listaSonidosPos.Add(sonidoHITPos6);
        listaSonidosPos.Add(sonidoHITPos7);
        listaSonidosPos.Add(sonidoHITPos8);

        audioFuente = cuerpoRefAudio.GetComponent<AudioSource>();
    }

    private IEnumerator StartWait()
    {
        yield return new WaitForSeconds(startWaitTime);
        animator.StopPlayback();
        FSM = new FukikaneWait();
        FSM.Initialize(this);
    }

    void Update()
    {
        FSM = FSM?.Process();
    }

    public override void GetHit(float damage, Vector3 effectPos)
    {
        life -= damage;
        lifeBar.value = life;


        if (life <= 0 && !animator.GetBool("death"))
        {
            StartCoroutine("Death");

            foreach (GameObject HB in GetComponentInChildren<EnemyFightExtra>().HBs)
            {
                HB.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject HB in GetComponentInChildren<EnemyFightExtra>().HBs)
            {
                HB.SetActive(false);
            }
        }

        Destroy(Instantiate(hitEffect, effectPos, Quaternion.identity), 1);

        SonidoHITPosAleatorio();
    }

    private IEnumerator Death()
    {
        animator.SetBool("death", true);
        FSM = null;
        yield return new WaitForSeconds(.1f);
        animator.SetBool("hasDied", true);

        FindObjectOfType<CambiarEscena>().ForceLoadScene();
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.velocity = Vector3.zero;
    }

    // Corrutina que produce sonido aleatorio del enemigo siendo golpeando
    public void SonidoHITPosAleatorio()
    {
        // Se escoge un sonido aleatorio de la lista
        int indice = Random.Range(0, listaSonidosPos.Count);
        AudioClip sonidoSeleccionado = listaSonidosPos[indice];

        // Reproducir el sonido
        audioFuente.PlayOneShot(sonidoSeleccionado);
    }
}
