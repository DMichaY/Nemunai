using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilState
{
    protected EvilFightAI enemyAI;
    protected float actionTimer;

    public void Initialize(EvilFightAI _enemyAI) { enemyAI = _enemyAI; }

    public enum States { WAIT, BLOCK, MOVE, ATTACK, DEATH }
    public enum Events { START, UPDATE, END }

    public States name;
    public Events currPhase;
    protected EvilState nextState;

    public EvilState() { }

    public virtual void Start() { currPhase = Events.UPDATE; }
    public virtual void Update() { currPhase = Events.UPDATE; }
    public virtual void End() { currPhase = Events.END; }

    public EvilState Process()
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
