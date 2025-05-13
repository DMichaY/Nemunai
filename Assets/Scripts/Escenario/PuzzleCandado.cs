using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCandado : MonoBehaviour
{

bool casa = false;

bool simbolo = false;

bool ojo = false;

bool espiral = false;


// hay que ponerlos en el gameobject anterior 
//ya que al clickar se mostrara el siguiente gameobject siendo visualmente el correcto

public void Casa()
{

   casa = true;

   Superado();


}

public void Simbolo()
{

   simbolo = true;

   Superado();


}

public void Ojo()
{

   ojo = true;

   Superado();


}

public void Espiral()
{

   espiral = true;

   Superado();


}


//botones restantes
public void CasaMal()
{

    casa = false;


}

public void SimboloMal()
{

    simbolo = false;


}

public void OjoMal()
{

    ojo = false;


}

public void EspiralMal()
{

    espiral = false;


}

public void Superado()
{

    if (casa && simbolo && ojo && espiral)
    {
        Debug.Log("superado");
    }


}

    

}
