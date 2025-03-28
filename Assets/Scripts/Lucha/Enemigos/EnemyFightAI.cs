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
    public float speed = 3.0f;
    public float waitTime;
    public float life = 100;

    //Extra
    private bool isActing;

    void Start()
    {
        player = FindObjectOfType<KaitoFight>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        lifeBar.maxValue = life;
        lifeBar.value = life;

        FSM = new EnemyMove();
        FSM.Initialize(this);
    }

    void Update()
    {
        FSM = FSM.Process();
    }

    public override void GetHit(float damage)
    {
        life -= damage;
        lifeBar.value = life;
    }
}
