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
    public GameObject hitEffect;
    EnemyFightExtra sonidoGolpeadoPos;
    KaitoFightExtra sonidoKaitoAtaque;
    private Vector2 movement;

    //Estad�sticas
    public float speed = 6, waitTime = 3, attackTime = 1, life = 100, startWaitTime = 0;

    public bool esPueblo;

    void Awake()
    {
        player = FindObjectOfType<KaitoFight>();
        animator = GetComponentInChildren<Animator>();
        animator.StartPlayback();
        rb = GetComponent<Rigidbody>();

        sonidoGolpeadoPos = FindObjectOfType<EnemyFightExtra>();
        sonidoKaitoAtaque = FindObjectOfType<KaitoFightExtra>();

        lifeBar.maxValue = life;
        lifeBar.value = life;

        StartCoroutine(StartWait());    
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
        
        // Actualizamos la posición en escena del enemigo y ejecutamos el sonido de pisadas correspondiente
        bool seMueve = rb.velocity.magnitude > 0.1f;
        
        sonidoGolpeadoPos.SonidosPisadas(seMueve);
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
            foreach(GameObject HB in GetComponentInChildren<EnemyFightExtra>().HBs)
            {
                HB.SetActive(false);
            }
        }

        Destroy(Instantiate(hitEffect, effectPos, Quaternion.identity), 1);

        sonidoGolpeadoPos.SonidoHITPosAleatorio();
        sonidoKaitoAtaque.SonidoATQKaitoAleatorio();
    }

    private IEnumerator Death()
    {
        FSM = null;
        animator.SetBool("death", true);
        yield return new WaitForSeconds(.3f);
        animator.SetBool("hasDied", true);

        if (esPueblo)
        {

            FindObjectOfType<DesactivarPelea>().Desactiva();
        }

        else
        {
            FindObjectOfType<CambiarEscena>().ForceLoadScene();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.velocity = Vector3.zero;
    }
}
