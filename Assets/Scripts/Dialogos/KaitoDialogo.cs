using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KaitoDialogo : MonoBehaviour
{
    [Header("Textos a mostrar en orden")]
    public List<TextMeshProUGUI> textos;

    [Header("Parámetros de animación")]
    public float velocidadEscritura = 0.05f;
    public float tiempoParaDesvanecer = 2f;
    public float tiempoEntreDesvanecimientos = 0.02f;
    public float intensidadTemblor = 1f;

    private int indiceTextoActual = 0;
    private TextMeshProUGUI textoActual;
    private Coroutine escribirCoroutine;
    private Coroutine desvanecerCoroutine;
    private bool jugadorDentro = false;
    private bool estaEscribiendo = false;
    private bool esperandoSiguiente = false;
    private bool salirDetectadoMientrasEscribe = false;

    public bool textoVisible = false;

    //Reiko
    public TextMeshProUGUI textoReiko;

    public TextMeshProUGUI textoDiario;


    void Start()
    {
        foreach (var texto in textos)
        {
            texto.gameObject.SetActive(false);
        }

        textoReiko.gameObject.SetActive(false);

        textoDiario.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        jugadorDentro = true;

        if (estaEscribiendo)
        {
            esperandoSiguiente = true;
            salirDetectadoMientrasEscribe = false;
            return;
        }

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

    public void MostrarSiguienteTexto()
    {
        textoVisible = true;

        if (textoActual != null)
        {
            textoActual.gameObject.SetActive(false);
        }

        textoActual = textos[indiceTextoActual];
        textoActual.gameObject.SetActive(true);

        if (escribirCoroutine != null)
            StopCoroutine(escribirCoroutine);

        escribirCoroutine = StartCoroutine(EscribirConEfecto(textoActual));
       
        indiceTextoActual = (indiceTextoActual + 1) % textos.Count;
    }

    public IEnumerator EscribirConEfecto(TextMeshProUGUI textoUI)
    {
        estaEscribiendo = true;
        esperandoSiguiente = false;

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

        estaEscribiendo = false;

        if (jugadorDentro && esperandoSiguiente)
        {
            MostrarSiguienteTexto();
        }
        else if (!jugadorDentro || salirDetectadoMientrasEscribe)
        {
            desvanecerCoroutine = StartCoroutine(DesvanecerTexto(textoUI));
        }
    }

    IEnumerator DesvanecerTexto(TextMeshProUGUI textoUI)
    {
        yield return new WaitForSeconds(tiempoParaDesvanecer);

        string texto = textoUI.text;
        string textoRestaurar = textoUI.text;

        for (int i = texto.Length - 1; i >= 0; i--)
        {
            texto = texto.Remove(i, 1);
            textoUI.text = texto;
            yield return new WaitForSeconds(tiempoEntreDesvanecimientos);
        }

        textoUI.text = textoRestaurar;
        textoUI.gameObject.SetActive(false);

        textoVisible = false;
    }




    public void TextoPuertaReiko()
    {
        
        textoVisible = true;

        if (textoActual != null)
        {
            textoActual.gameObject.SetActive(false);
        }

        textoActual = textoReiko;
        textoActual.gameObject.SetActive(true);

        if (escribirCoroutine != null)
            StopCoroutine(escribirCoroutine);

        escribirCoroutine = StartCoroutine(EscribirConEfecto(textoActual));
    }


    public void TextoPuertaDiario()
    {

        textoVisible = true;

        if (textoActual != null)
        {
            textoActual.gameObject.SetActive(false);
        }

        textoActual = textoDiario;
        textoActual.gameObject.SetActive(true);

        if (escribirCoroutine != null)
            StopCoroutine(escribirCoroutine);

        escribirCoroutine = StartCoroutine(EscribirConEfecto(textoActual));
    }
}
