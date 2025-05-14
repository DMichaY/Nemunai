using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public float velocidadEscritura = 0.05f;
    public float tiempoParaDesvanecer = 2f;
    public float tiempoEntreDesvanecimientos = 0.02f;

    private Coroutine escribirCoroutine;
    private Coroutine desvanecerCoroutine;

    public void MostrarTexto(TextMeshProUGUI textoUI)
    {
        if (escribirCoroutine != null)
            StopCoroutine(escribirCoroutine);
        if (desvanecerCoroutine != null)
            StopCoroutine(desvanecerCoroutine);

        textoUI.gameObject.SetActive(true);
        escribirCoroutine = StartCoroutine(EscribirConEfecto(textoUI));
    }

    private IEnumerator EscribirConEfecto(TextMeshProUGUI textoUI)
    {
        string textoCompleto = textoUI.text;
        textoUI.maxVisibleCharacters = 0;
        textoUI.text = textoCompleto;

        for (int i = 0; i <= textoCompleto.Length; i++)
        {
            textoUI.maxVisibleCharacters = i;
            yield return new WaitForSeconds(velocidadEscritura);
        }

        yield return new WaitForSeconds(tiempoParaDesvanecer);
        desvanecerCoroutine = StartCoroutine(DesvanecerTexto(textoUI));
    }

    private IEnumerator DesvanecerTexto(TextMeshProUGUI textoUI)
    {
        string texto = textoUI.text;
        for (int i = texto.Length; i >= 0; i--)
        {
            textoUI.maxVisibleCharacters = i;
            yield return new WaitForSeconds(tiempoEntreDesvanecimientos);
        }

        textoUI.gameObject.SetActive(false);
    }
}
