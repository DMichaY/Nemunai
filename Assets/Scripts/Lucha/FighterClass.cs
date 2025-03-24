using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FighterClass : MonoBehaviour
{
    //Esta clase sirve para poder hacer referencia a funciones GetHit de cada luchador

    public abstract void GetHit(float damage);
}
