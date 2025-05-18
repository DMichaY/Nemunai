using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukikaneState
{
    protected FukikaneAI enemyAI;
    protected float actionTimer;

    public void Initialize(FukikaneAI _enemyAI) { enemyAI = _enemyAI; }

    public enum States { WAIT, MOVE, ATTACK, DEATH }
    public enum Events { START, UPDATE, END }

    public States name;
    public Events currPhase;
    protected FukikaneState nextState;

    public FukikaneState() { }

    public virtual void Start() { currPhase = Events.UPDATE; }
    public virtual void Update() { currPhase = Events.UPDATE; }
    public virtual void End() { currPhase = Events.END; }

    public FukikaneState Process()
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
