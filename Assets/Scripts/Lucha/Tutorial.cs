using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public float timeUntilDestruction = 7;
    void Start()
    {
        Destroy(gameObject, timeUntilDestruction);
    }
}
