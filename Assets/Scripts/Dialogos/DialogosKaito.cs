using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogosKaito : MonoBehaviour
{
    [Header("Textos a mostrar en orden")]
    public List<TextMeshProUGUI> textos;

    [Header("Parámetros de animación")]
    public float velocidadEscritura = 0.05f;
    public float tiempoParaDesvanecer = 2f;
    public float tiempoEntreDesvanecimientos = 0.02f;
    public float intensidadTemblor = 1f;

    private static int indiceGlobal = 0;

    private TextMeshProUGUI textoActual;
    private Coroutine escribirCoroutine;
    private Coroutine desvanecerCoroutine;
    private bool jugadorDentro = false;
    private bool estaEscribiendo = false;
    private bool salirDetectadoMientrasEscribe = false;

    public bool textoVisible = false;

    void Start()
    {
        foreach (var texto in textos)
        {
            texto.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        jugadorDentro = true;
        CancelarTextoActual(true);
        MostrarSiguienteTexto();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        jugadorDentro = false;

        if (estaEscribiendo)
        {
            salirDetectadoMientrasEscribe = true;
        }
        else if (textoActual != null)
        {
            desvanecerCoroutine = StartCoroutine(DesvanecerTexto(textoActual));
        }
    }

    void MostrarSiguienteTexto()
    {
        if (textoActual != null)
        {
            textoActual.gameObject.SetActive(false);
        }

        // Obtener el siguiente texto globalmente
        if (indiceGlobal >= textos.Count)
        {
            indiceGlobal = 0; // reinicia si se pasa
        }

        textoActual = textos[indiceGlobal];
        textoActual.gameObject.SetActive(true);
        indiceGlobal++;

        escribirCoroutine = StartCoroutine(EscribirConEfecto(textoActual));
    }

    IEnumerator EscribirConEfecto(TextMeshProUGUI textoUI)
    {
        estaEscribiendo = true;
        textoVisible = true;

        string textoCompleto = textoUI.text;
        textoUI.text = "";
        textoUI.ForceMeshUpdate();

        for (int i = 0; i < textoCompleto.Length; i++)
        {
            textoUI.text += textoCompleto[i];
            textoUI.ForceMeshUpdate();

            TMP_TextInfo textInfo = textoUI.textInfo;
            int charIndex = textoUI.text.Length - 1;

            if (charIndex < textInfo.characterCount)
            {
                var charInfo = textInfo.characterInfo[charIndex];
                if (charInfo.isVisible)
                {
                    int vertexIndex = charInfo.vertexIndex;
                    Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                    float offsetX = Random.Range(-intensidadTemblor, intensidadTemblor);
                    float offsetY = Random.Range(-intensidadTemblor, intensidadTemblor);

                    for (int j = 0; j < 4; j++)
                    {
                        vertices[vertexIndex + j] += new Vector3(offsetX, offsetY, 0);
                    }

                    textoUI.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
                }
            }

            yield return new WaitForSeconds(velocidadEscritura);
        }

        estaEscribiendo = false;

        if (!jugadorDentro || salirDetectadoMientrasEscribe)
        {
            desvanecerCoroutine = StartCoroutine(DesvanecerTexto(textoUI));
        }
    }

    IEnumerator DesvanecerTexto(TextMeshProUGUI textoUI, bool rapido = false)
    {
        float delay = rapido ? 0f : tiempoParaDesvanecer;
        yield return new WaitForSeconds(delay);

        int totalChars = textoUI.text.Length;
        float intervalo = rapido ? 0.001f : tiempoEntreDesvanecimientos;

        for (int i = totalChars; i >= 0; i--)
        {
            textoUI.maxVisibleCharacters = i;
            yield return new WaitForSeconds(intervalo);
        }

        textoUI.text = "";
        textoUI.gameObject.SetActive(false);
        textoVisible = false;
    }

    void CancelarTextoActual(bool rapido)
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

        estaEscribiendo = false;
        salirDetectadoMientrasEscribe = false;
    }
}
