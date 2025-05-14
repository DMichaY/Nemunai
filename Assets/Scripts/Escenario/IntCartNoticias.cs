using UnityEngine;
using System.Collections;
using TMPro;

public class IntCartNoticias : Interactable
{
    LogicaVerjaEstacion puertaEstacion;
    public TextMeshProUGUI textoNoticias;
    private TypewriterEffect typewriter;

    void Start()
    {
        puertaEstacion = GameObject.Find("TriggerPuerta").GetComponent<LogicaVerjaEstacion>();
        typewriter = gameObject.AddComponent<TypewriterEffect>();
        textoNoticias.gameObject.SetActive(false);
    }

    public override void Interact()
    {
        if (!puertaEstacion.antiSpam)
        {
            puertaEstacion.antiSpam = true;
            typewriter.MostrarTexto(textoNoticias);
            StartCoroutine(ResetAntiSpam());
        }
    }

    private IEnumerator ResetAntiSpam()
    {
        yield return new WaitForSeconds(5f);
        puertaEstacion.antiSpam = false;
    }
}
