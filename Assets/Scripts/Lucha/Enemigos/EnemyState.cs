using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyFightAI enemyAI;

    public void Initialize(EnemyFightAI _enemyAI) { enemyAI = _enemyAI; }

    public enum States { WAIT, MOVE, ATTACK, DEATH }
    public enum Events { START, UPDATE, END }

    public States name;
    public Events currPhase;
    protected EnemyState nextState;

    public EnemyState() { }

    public virtual void Start() { currPhase = Events.UPDATE; }
    public virtual void Update() { currPhase = Events.UPDATE; }
    public virtual void End() { currPhase = Events.END; }

    public EnemyState Process()
    {
        if (currPhase == Events.START) Start();
        if (currPhase == Events.UPDATE) Update();
        if (currPhase == Events.END)
        {
            End();
            return nextState;
        }
        return this;
    }
}
