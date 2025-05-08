using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleInvertido : MonoBehaviour
{

    public Button button;

    public Color newColor;

    public GameObject botones;



    /*public void Start()
    {
        foreach (var item in collection)
        {

        }


    }*/

    public void Pulsar()
    {

        ColorBlock cb = button.colors;

        cb.normalColor = newColor;

        cb.highlightedColor = newColor;

        button.colors = cb;


    }
}
