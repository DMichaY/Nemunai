using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EvilFightAI : FighterClass
{
    //Referencias
    public Slider lifeBar;
    public KaitoFight player;
    public EvilState FSM;
    public Animator animator;
    public Rigidbody rb;
    public GameObject hitEffect, blockEffect;
    EvilFightExtra sonidoGolpeadoKaitoO;
    KaitoFightExtra sonidoKaitoAtaque;
    public bool isBlocking;

    //Estad�sticas
    public float speed = 6, waitTime = 3, attackTime = 1, life = 100, startWaitTime = 0;

    public bool esPueblo;

    void Awake()
    {
        player = FindObjectOfType<KaitoFight>();
        animator = GetComponentInChildren<Animator>();
        animator.StartPlayback();
        rb = GetComponent<Rigidbody>();

        sonidoGolpeadoKaitoO = FindObjectOfType<EvilFightExtra>();
        sonidoKaitoAtaque = FindObjectOfType<KaitoFightExtra>();

        lifeBar.maxValue = life;
        lifeBar.value = life;

        StartCoroutine(StartWait());    
    }

    private IEnumerator StartWait()
    {
        yield return new WaitForSeconds(startWaitTime);
        animator.StopPlayback();
        FSM = new EvilWait();
        FSM.Initialize(this);
    }

    void Update()
    {
        FSM = FSM?.Process();
    }

    public override void GetHit(float damage, Vector3 effectPos)
    {
        if (!isBlocking)
        {
            life -= damage;
            lifeBar.value = life;


            if (life <= 0 && !animator.GetBool("death"))
            {
                StartCoroutine("Death");

                foreach (GameObject HB in GetComponentInChildren<EvilFightExtra>().HBs)
                {
                    HB.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject HB in GetComponentInChildren<EvilFightExtra>().HBs)
                {
                    HB.SetActive(false);
                }
            }

            Destroy(Instantiate(hitEffect, effectPos, Quaternion.identity), 1);

            sonidoGolpeadoKaitoO.SonidoHITKaitoOAleatorio();
            sonidoKaitoAtaque.SonidoATQKaitoAleatorio();
        }
        else
        {
            Destroy(Instantiate(blockEffect, effectPos, Quaternion.identity), 1);
        }
    }

    private IEnumerator Death()
    {
        FSM = null;
        animator.SetBool("death", true);
        yield return new WaitForSeconds(.2f);
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
}
