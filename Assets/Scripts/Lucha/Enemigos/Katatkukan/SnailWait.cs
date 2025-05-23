using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailWait : SnailState
{

    public SnailWait() : base()
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
            if (!IsPlayerClose()) nextState = new SnailMove();
            else nextState = new SnailAttack();

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
        return Vector3.Distance(enemyAI.transform.position, enemyAI.player.transform.position) <= 15;
    }
}
