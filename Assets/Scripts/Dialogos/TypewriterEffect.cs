using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TypewriterEffect : MonoBehaviour
{
    public float velocidadEscritura = 0.05f;
    public float tiempoParaDesvanecer = 2f;
    public float tiempoEntreDesvanecimientos = 0.02f;
    public float intensidadTemblor = 0.5f;

    private Coroutine escribirCoroutine;
    private Coroutine desvanecerCoroutine;
    private TextMeshProUGUI textoActual;

    private bool temblorActivo = false;

    public void MostrarTexto(TextMeshProUGUI textoUI)
    {
        CancelarTexto(true);

        textoActual = textoUI;
        textoUI.gameObject.SetActive(true);
        escribirCoroutine = StartCoroutine(EscribirConEfecto(textoUI));
    }

    private IEnumerator EscribirConEfecto(TextMeshProUGUI textoUI)
    {
        string textoCompleto = textoUI.text;
        textoUI.maxVisibleCharacters = 0;
        textoUI.text = textoCompleto;

        temblorActivo = true;
        StartCoroutine(TemblorTexto(textoUI));

        for (int i = 0; i <= textoCompleto.Length; i++)
        {
            textoUI.maxVisibleCharacters = i;
            yield return new WaitForSeconds(velocidadEscritura);
        }

        yield return new WaitForSeconds(tiempoParaDesvanecer);
        desvanecerCoroutine = StartCoroutine(DesvanecerTexto(textoUI, false));
    }

    private IEnumerator DesvanecerTexto(TextMeshProUGUI textoUI, bool rapido)
    {
        temblorActivo = false;

        string texto = textoUI.text;
        float intervalo = rapido ? 0.001f : tiempoEntreDesvanecimientos;

        for (int i = texto.Length; i >= 0; i--)
        {
            textoUI.maxVisibleCharacters = i;
            yield return new WaitForSeconds(intervalo);
        }

        textoUI.gameObject.SetActive(false);
        textoActual = null;
    }

    public void CancelarTexto(bool rapido)
    {
        if (escribirCoroutine != null)
        {
            StopCoroutine(escribirCoroutine);
            escribirCoroutine = null;
        }

        if (desvanecerCoroutine != null)
        {
            StopCoroutine(desvanecerCoroutine);
            desvanecerCoroutine = null;
        }

        if (textoActual != null && textoActual.gameObject.activeSelf)
        {
            desvanecerCoroutine = StartCoroutine(DesvanecerTexto(textoActual, rapido));
        }
    }

    private IEnumerator TemblorTexto(TextMeshProUGUI textoUI)
    {
        TMP_TextInfo textInfo = textoUI.textInfo;

        while (temblorActivo)
        {
            textoUI.ForceMeshUpdate();
            textInfo = textoUI.textInfo;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (!textInfo.characterInfo[i].isVisible) continue;

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                Vector3 offset = new Vector3(
                    Random.Range(-intensidadTemblor, intensidadTemblor),
                    Random.Range(-intensidadTemblor, intensidadTemblor),
                    0);

                vertices[vertexIndex + 0] += offset;
                vertices[vertexIndex + 1] += offset;
                vertices[vertexIndex + 2] += offset;
                vertices[vertexIndex + 3] += offset;
            }

            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                textoUI.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            yield return new WaitForSeconds(0.025f);
        }
    }
}
