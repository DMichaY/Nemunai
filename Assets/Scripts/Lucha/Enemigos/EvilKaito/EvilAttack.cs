using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilAttack : EvilState
{
    private float attackTimer;

    public EvilAttack() : base()
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
        if (actionTimer >= enemyAI.waitTime && !enemyAI.animator.GetBool("isAttacking"))
        {
            enemyAI.animator.ResetTrigger("punchLeft");
            enemyAI.animator.ResetTrigger("punchRight");
            enemyAI.animator.ResetTrigger("kickLeft");
            enemyAI.animator.ResetTrigger("kickRight");
            nextState = new EvilWait();
            nextState.Initialize(enemyAI);
            currPhase = Events.END;
        }
        else if (attackTimer > enemyAI.attackTime)
        {
            attackTimer = 0;
            enemyAI.animator.SetBool("isAttacking", true);

            switch (Random.Range(0, 4))
            {
                case 0:
                    enemyAI.animator.SetTrigger("punchLeft");
                    return;

                case 1:
                    enemyAI.animator.SetTrigger("punchRight");
                    return;

                case 2:
                    enemyAI.animator.SetTrigger("kickLeft");
                    return;

                case 3:
                    enemyAI.animator.SetTrigger("kickRight");
                    return;
            }
        }

        attackTimer += Time.deltaTime;
        actionTimer += Time.deltaTime;
    }

    public override void End()
    {
        base.End();
    }
}
