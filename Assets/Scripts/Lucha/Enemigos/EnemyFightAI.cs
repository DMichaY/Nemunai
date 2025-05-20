using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyFightAI : FighterClass
{
    //Referencias
    public Slider lifeBar;
    public KaitoFight player;
    public EnemyState FSM;
    public Animator animator;
    public Rigidbody rb;

    public AudioClip sonidoHITPos1;
    public AudioClip sonidoHITPos2;
    public AudioClip sonidoHITPos3;
    public AudioClip sonidoHITPos4;

    AudioSource audioFuente;

    public GameObject cuerpoRefAudio;

    private List<AudioClip> listaSonidosHITPos = new List<AudioClip>();

    //Estad�sticas
    public float speed = 6, waitTime = 3, attackTime = 1, life = 100, startWaitTime = 0;

    public bool esPueblo;

    void Awake()
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
        listaSonidosHITPos.Add(sonidoHITPos1);
        listaSonidosHITPos.Add(sonidoHITPos2);
        listaSonidosHITPos.Add(sonidoHITPos3);
        listaSonidosHITPos.Add(sonidoHITPos4);

        audioFuente = cuerpoRefAudio.GetComponent<AudioSource>();
    }

    private IEnumerator StartWait()
    {
        yield return new WaitForSeconds(startWaitTime);
        animator.StopPlayback();
        FSM = new EnemyWait();
        FSM.Initialize(this);
    }

    void Update()
    {
        FSM = FSM?.Process();
    }

    public override void GetHit(float damage)
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
            foreach(GameObject HB in GetComponentInChildren<EnemyFightExtra>().HBs)
            {
                HB.SetActive(false);
            }
        }

        SonidoHITPosAleatorio();
    }

    private IEnumerator Death()
    {
        animator.SetBool("death", true);
        FSM = null;
        yield return new WaitForSeconds(.1f);
        animator.SetBool("hasDied", true);

        if (esPueblo)
        {

            FindObjectOfType<DesactivarPelea>().Desactiva();
        }

        else
        {
            //S�lo para el release 1
            FindObjectOfType<CambiarEscena>().ForceLoadScene();
        }

        
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.velocity = Vector3.zero;
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
}
