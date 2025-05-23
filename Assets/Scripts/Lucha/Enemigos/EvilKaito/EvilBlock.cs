using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBlock : EvilState
{

    public EvilBlock() : base()
    {
        name = States.BLOCK;
    }

    public override void Start()
    {
        actionTimer = 0;
        enemyAI.animator.SetBool("crouch", true);
        enemyAI.isBlocking = true;
        base.Start();
    }

    public override void Update()
    {
        if (actionTimer >= enemyAI.waitTime)
        {
            if (IsPlayerClose())
            {
                nextState = new EvilAttack();

                nextState.Initialize(enemyAI);
                currPhase = Events.END;
            }
            else
            {
                nextState = new EvilMove();

                nextState.Initialize(enemyAI);
                currPhase = Events.END;
            }
        }
        actionTimer += Time.deltaTime;
    }

    public override void End()
    {
        enemyAI.animator.SetBool("crouch", false);
        enemyAI.isBlocking = false;
        base.End();
    }

    private bool IsPlayerClose()
    {
        return Vector3.Distance(enemyAI.transform.position, enemyAI.player.transform.position) <= 5;
    }
}
