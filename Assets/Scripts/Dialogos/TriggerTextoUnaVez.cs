using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class TriggerTextoUnaVez : MonoBehaviour
{
    public TextMeshProUGUI textoTMP;
    public float velocidadEscritura = 0.05f;
    public float tiempoEsperaAntesDesvanecer = 2f;
    public float tiempoEntreDesvanecimientos = 0.02f;
    public float intensidadTemblor = 1f;

    private bool activado = false;
    private Coroutine escribirCoroutine;
    private Coroutine desvanecerCoroutine;

    private bool fueInterrumpido = false;
    private static TriggerTextoUnaVez textoActivo;

    private void Start()
    {
        if (textoTMP != null)
        {
            textoTMP.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || activado || textoTMP == null) return;

        // Si otro texto está activo, cancelarlo rápidamente
        if (textoActivo != null && textoActivo != this)
        {
            textoActivo.CancelarTextoActual(rapido: true);
        }

        textoActivo = this;
        activado = true;
        textoTMP.gameObject.SetActive(true);
        escribirCoroutine = StartCoroutine(EscribirConEfecto(textoTMP));
    }

    private IEnumerator EscribirConEfecto(TextMeshProUGUI textoUI)
    {
        fueInterrumpido = false;

        string textoCompleto = textoUI.text;
        textoUI.text = "";

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

        if (!fueInterrumpido)
        {
            yield return new WaitForSeconds(tiempoEsperaAntesDesvanecer);
            desvanecerCoroutine = StartCoroutine(DesvanecerTexto(textoUI));
        }
    }

    IEnumerator DesvanecerTexto(TextMeshProUGUI textoUI, bool rapido = false)
    {
        string texto = textoUI.text;
        string textoOriginal = texto;

        float intervalo = rapido ? 0.001f : tiempoEntreDesvanecimientos;

        for (int i = texto.Length - 1; i >= 0; i--)
        {
            texto = texto.Remove(i, 1);
            textoUI.text = texto;
            yield return new WaitForSeconds(intervalo);
        }

        textoUI.text = textoOriginal;
        textoUI.gameObject.SetActive(false);

        // Limpieza final
        if (textoActivo == this)
        {
            textoActivo = null;
        }

        Destroy(gameObject);
    }

    public void CancelarTextoActual(bool rapido)
    {
        if (escribirCoroutine != null)
        {
            StopCoroutine(escribirCoroutine);
            escribirCoroutine = null;
            fueInterrumpido = true;
        }

        if (desvanecerCoroutine != null)
        {
            StopCoroutine(desvanecerCoroutine);
        }

        if (textoTMP != null && textoTMP.gameObject.activeSelf)
        {
            desvanecerCoroutine = StartCoroutine(DesvanecerTexto(textoTMP, rapido));
        }
    }
}
