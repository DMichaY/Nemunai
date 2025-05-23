using UnityEngine;
using TMPro;
using System.Collections;

public class AnastasiaTexto : Interactable
{

    public TextMeshProUGUI texto;
    private TypewriterEffect typewriter;

    private bool spam = false;

    void Start()
    {
        
        typewriter = gameObject.AddComponent<TypewriterEffect>();
        texto.gameObject.SetActive(false);
    }


    public override void Interact()
    {
        if (!spam)
        {

            spam = true;

            typewriter.MostrarTexto(texto);

            StartCoroutine(ResetAntiSpam());

        }


            
    }


    private IEnumerator ResetAntiSpam()
    {
        yield return new WaitForSeconds(7f);
        spam = false;
    }
}
