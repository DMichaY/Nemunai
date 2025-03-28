using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : EnemyState
{
    public EnemyMove() : base()
    {
        name = States.MOVE;
    }

    public override void Start()
    {
        Debug.Log("MOVERSE");
        actionTimer = 0;
        base.Start();
    }

    public override void Update()
    {
        if (IsPlayerClose())
        {
            enemyAI.rb.velocity = Vector3.zero;
            nextState = new EnemyAttack();
            nextState.Initialize(enemyAI);
            currPhase = Events.END;
        }
        else
        {
            enemyAI.animator.SetBool("goRight", true);
            enemyAI.rb.velocity = new Vector3(0, 0, -enemyAI.speed);
        }

        if (actionTimer >= enemyAI.waitTime)
        {
            enemyAI.rb.velocity = Vector3.zero;

            nextState = new EnemyWait();
            nextState.Initialize(enemyAI);
            currPhase = Events.END;
        }
        actionTimer += Time.deltaTime;
    }

    public override void End()
    {
        enemyAI.animator.SetBool("goRight", false);
        base.End();
    }

    private bool IsPlayerClose()
    {
        return Vector3.Distance(enemyAI.transform.position, enemyAI.player.transform.position) <= 7;
    }
}
