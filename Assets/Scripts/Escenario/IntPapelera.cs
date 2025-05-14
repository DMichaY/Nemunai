using UnityEngine;
using TMPro;
using System.Collections;


public class IntPapelera : Interactable
{
    LogicaVerjaEstacion puertaEstacion;
    public TextMeshProUGUI textoPapelera;
    private TypewriterEffect typewriter;

    void Start()
    {
        puertaEstacion = GameObject.Find("TriggerPuerta").GetComponent<LogicaVerjaEstacion>();
        typewriter = gameObject.AddComponent<TypewriterEffect>();
        textoPapelera.gameObject.SetActive(false);
    }

    public override void Interact()
    {
        if (!puertaEstacion.antiSpam)
        {
            puertaEstacion.antiSpam = true;
            typewriter.MostrarTexto(textoPapelera);
            StartCoroutine(ResetAntiSpam());
        }
    }

    private IEnumerator ResetAntiSpam()
    {
        yield return new WaitForSeconds(5f);
        puertaEstacion.antiSpam = false;
    }
}
