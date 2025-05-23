using UnityEngine;
using System.Collections;

using TMPro;

public class IntMaquina : Interactable
{
    LogicaVerjaEstacion puertaEstacion;
    public TextMeshProUGUI textoMaquina;
    private TypewriterEffect typewriter;

    void Start()
    {
        puertaEstacion = GameObject.Find("PuertaVerja").GetComponent<LogicaVerjaEstacion>();
        typewriter = gameObject.AddComponent<TypewriterEffect>();
        textoMaquina.gameObject.SetActive(false);
    }

    public override void Interact()
    {
        if (!puertaEstacion.antiSpam)
        {
            puertaEstacion.antiSpam = true;
            typewriter.MostrarTexto(textoMaquina);
            StartCoroutine(ResetAntiSpam());
        }
    }

    private IEnumerator ResetAntiSpam()
    {
        yield return new WaitForSeconds(5f);
        puertaEstacion.antiSpam = false;
    }
}
