using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWait : EnemyState
{

    public EnemyWait() : base()
    {
        name = States.WAIT;
    }

    public override void Start()
    {
        actionTimer = 0;
        base.Start();
    }

    public override void Update()
    {
        if (actionTimer >= enemyAI.waitTime)
        {
            if (!IsPlayerClose()) nextState = new EnemyMove();
            else nextState = new EnemyAttack();

            nextState.Initialize(enemyAI);
            currPhase = Events.END;
        }
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
