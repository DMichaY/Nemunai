using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMove : SnailState
{
    private bool moveForward;

    public SnailMove() : base()
    {
        name = States.MOVE;
    }

    public override void Start()
    {
        actionTimer = 0;
        if (Random.Range(0, 3) != 2) moveForward = true;
        else moveForward = false;
        base.Start();
    }

    public override void Update()
    {
        if (IsPlayerClose())
        {
            enemyAI.rb.velocity = Vector3.zero;
            nextState = new SnailAttack();
            nextState.Initialize(enemyAI);
            currPhase = Events.END;
        }
        else
        {
            if(moveForward)
            {
                enemyAI.animator.SetBool("goForward", true);
                enemyAI.rb.velocity = new Vector3(0, 0, -enemyAI.speed);
            }
            else
            {
                enemyAI.animator.SetBool("goBackward", true);
                enemyAI.rb.velocity = new Vector3(0, 0, enemyAI.speed);
            }
        }

        if (actionTimer >= enemyAI.waitTime)
        {
            enemyAI.rb.velocity = Vector3.zero;

            nextState = new SnailWait();
            nextState.Initialize(enemyAI);
            currPhase = Events.END;
        }
        actionTimer += Time.deltaTime;
    }

    public override void End()
    {
        enemyAI.animator.SetBool("goForward", false);
        enemyAI.animator.SetBool("goBackward", false);
        base.End();
    }

    private bool IsPlayerClose()
    {
        return Vector3.Distance(enemyAI.transform.position, enemyAI.player.transform.position) <= 15;
    }
}
