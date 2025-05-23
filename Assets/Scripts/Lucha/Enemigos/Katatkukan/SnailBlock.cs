using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailBlock : SnailState
{
    private bool stopAnimationPlaying = false;

    public SnailBlock() : base()
    {
        name = States.BLOCK;
    }

    public override void Start()
    {
        actionTimer = -2.1f;
        enemyAI.animator.SetTrigger("switchBlock");
        base.Start();
    }

    public override void Update()
    {
        if (actionTimer >= enemyAI.waitTime && !stopAnimationPlaying)
        {
            enemyAI.animator.SetTrigger("switchBlock");
            stopAnimationPlaying = true;
            actionTimer = 0;
        }
        else if (actionTimer >= 2.1f && stopAnimationPlaying)
        {
            stopAnimationPlaying = false;

            if (IsPlayerClose())
            {
                nextState = new SnailAttack();

                nextState.Initialize(enemyAI);
                currPhase = Events.END;
            }
            else
            {
                nextState = new SnailMove();

                nextState.Initialize(enemyAI);
                currPhase = Events.END;
            }
        }
        actionTimer += Time.deltaTime;
    }

    public override void End()
    {
        enemyAI.isBlocking = false;
        base.End();
    }

    private bool IsPlayerClose()
    {
        return Vector3.Distance(enemyAI.transform.position, enemyAI.player.transform.position) <= 15;
    }
}
