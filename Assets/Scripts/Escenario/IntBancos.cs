using UnityEngine;
using TMPro;
using System.Collections;


public class IntBancos : Interactable
{
    LogicaVerjaEstacion puertaEstacion;
    public TextMeshProUGUI textoBancos;
    private TypewriterEffect typewriter;

    void Start()
    {
        puertaEstacion = GameObject.Find("TriggerPuerta").GetComponent<LogicaVerjaEstacion>();
        typewriter = gameObject.AddComponent<TypewriterEffect>();
        textoBancos.gameObject.SetActive(false);
    }

    public override void Interact()
    {
        if (!puertaEstacion.antiSpam)
        {
            puertaEstacion.antiSpam = true;
            typewriter.MostrarTexto(textoBancos);
            StartCoroutine(ResetAntiSpam());
        }
    }

    private IEnumerator ResetAntiSpam()
    {
        yield return new WaitForSeconds(5f);
        puertaEstacion.antiSpam = false;
    }
}
