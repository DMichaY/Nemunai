using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMove : EvilState
{
    private bool moveForward;

    public EvilMove() : base()
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
            nextState = new EvilAttack();
            nextState.Initialize(enemyAI);
            currPhase = Events.END;
        }
        else
        {
            if(moveForward)
            {
                enemyAI.animator.SetBool("goRight", true);
                enemyAI.rb.velocity = new Vector3(0, 0, -enemyAI.speed);
            }
            else
            {
                enemyAI.animator.SetBool("goLeft", true);
                enemyAI.rb.velocity = new Vector3(0, 0, enemyAI.speed);
            }
        }

        if (actionTimer >= enemyAI.waitTime)
        {
            enemyAI.rb.velocity = Vector3.zero;

            nextState = new EvilWait();
            nextState.Initialize(enemyAI);
            currPhase = Events.END;
        }
        actionTimer += Time.deltaTime;
    }

    public override void End()
    {
        enemyAI.animator.SetBool("goRight", false);
        enemyAI.animator.SetBool("goLeft", false);
        base.End();
    }

    private bool IsPlayerClose()
    {
        return Vector3.Distance(enemyAI.transform.position, enemyAI.player.transform.position) <= 5;
    }
}
