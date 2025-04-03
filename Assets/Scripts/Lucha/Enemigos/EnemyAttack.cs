using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyState
{
    private float attackTimer;

    public EnemyAttack() : base()
    {
        name = States.ATTACK;
    }

    public override void Start()
    {
        actionTimer = 0;
        attackTimer = enemyAI.attackTime;
        base.Start();
    }

    public override void Update()
    {
        if (!IsPlayerClose() || actionTimer >= enemyAI.waitTime)
        {
            enemyAI.animator.ResetTrigger("attack1");
            enemyAI.animator.ResetTrigger("attack2");
            nextState = new EnemyWait();
            nextState.Initialize(enemyAI);
            currPhase = Events.END;
        }
        else if (attackTimer > enemyAI.attackTime)
        {
            attackTimer = 0;
            enemyAI.animator.SetBool("isAttacking", true);

            if(Random.Range(0, 2) == 0) enemyAI.animator.SetTrigger("attack1");
            else enemyAI.animator.SetTrigger("attack2");
        }

        attackTimer += Time.deltaTime;
        actionTimer += Time.deltaTime;
    }

    public override void End()
    {
        base.End();
    }

    private bool IsPlayerClose()
    {
        return Vector3.Distance(enemyAI.transform.position, enemyAI.player.transform.position) <= 7;
    }
}
