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

    FukikaneFightExtra sonidoGolpeadoFuk;
    KaitoFightExtra sonidoKaitoAtaque;

    //Estadísticas
    public float speed = 6, waitTime = 3, attackTime = 1, life = 100, startWaitTime = 0;

    void Start()
    {
        player = FindObjectOfType<KaitoFight>();
        animator = GetComponentInChildren<Animator>();
        animator.StartPlayback();
        rb = GetComponent<Rigidbody>();

        sonidoGolpeadoFuk = FindObjectOfType<FukikaneFightExtra>();
        sonidoKaitoAtaque = FindObjectOfType<KaitoFightExtra>();

        lifeBar.maxValue = life;
        lifeBar.value = life;

        StartCoroutine(StartWait());
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

            foreach (GameObject HB in GetComponentInChildren<FukikaneFightExtra>().HBs)
            {
                HB.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject HB in GetComponentInChildren<FukikaneFightExtra>().HBs)
            {
                HB.SetActive(false);
            }
        }

        Destroy(Instantiate(hitEffect, effectPos, Quaternion.identity), 1);

        sonidoGolpeadoFuk.SonidoHITFukAleatorio();
        sonidoKaitoAtaque.SonidoATQKaitoAleatorio();
    }

    private IEnumerator Death()
    {
        animator.SetBool("death", true);
        FSM = null;
        yield return new WaitForSeconds(.1f);
        animator.SetBool("hasDied", true);

        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<CambiarEscena>().ForceLoadScene();
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.velocity = Vector3.zero;
    }
}
