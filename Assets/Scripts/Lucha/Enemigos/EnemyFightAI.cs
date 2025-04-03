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

    //Estadísticas
    public float speed = 6, waitTime = 3, attackTime = 1;
    public float life = 100;

    void Start()
    {
        player = FindObjectOfType<KaitoFight>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        lifeBar.maxValue = life;
        lifeBar.value = life;

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
        }
        else
        {
            if (Random.Range(0, 2) == 0) animator.SetTrigger("damage1");
            else animator.SetTrigger("damage2");
        }
    }

    private IEnumerator Death()
    {
        animator.SetBool("death", true);
        FSM = null;
        yield return new WaitForSeconds(.1f);
        animator.SetBool("hasDied", true);

        //Sólo para el release 1
        FindObjectOfType<CambiarEscena>().ForceLoadScene();
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.velocity = Vector3.zero;
    }
}
