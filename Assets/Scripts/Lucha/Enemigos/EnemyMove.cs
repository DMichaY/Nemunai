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
        base.Start();
    }

    public override void Update()
    {
        if (IsPlayerClose())
        {
            nextState = new EnemyAttack();
            nextState.Initialize(enemyAI);
            currPhase = Events.END;
        }
        else
        {
            //enemyAI.animator.SetBool("", true);
        }
    }

    public override void End()
    {
        base.End();
    }

    private bool IsPlayerClose()
    {
        return Vector3.Distance(enemyAI.transform.position, enemyAI.player.transform.position) <= 10;
    }
}
