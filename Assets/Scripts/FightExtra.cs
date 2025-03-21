using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightExtra : MonoBehaviour
{
    private KaitoFight kaitoScript;

    private void Awake()
    {
        kaitoScript= FindObjectOfType<KaitoFight>();
    }

    public void ActivateMovement()
    {
        kaitoScript.ActivateMovement();
    }
}
