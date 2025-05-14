using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class TriggerTextoUnaVez : MonoBehaviour
{
    public TextMeshProUGUI textoTMP;
    public float velocidadEscritura = 0.05f;
    public float tiempoEsperaAntesDesvanecer = 2f;
    public float duracionDesvanecer = 1.5f;

    private bool activado = false;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        if (textoTMP != null)
        {
            textoTMP.gameObject.SetActive(false);

            // Asegurar que tiene un CanvasGroup para el fade out
            canvasGroup = textoTMP.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = textoTMP.gameObject.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activado || textoTMP == null) return;

        if (other.CompareTag("Player"))
        {
            activado = true;
            StartCoroutine(MostrarTexto());
        }
    }

    IEnumerator MostrarTexto()
    {
        // Obtener el texto que ya tiene el TMP asignado
        string mensaje = textoTMP.text;
        textoTMP.text = "";  // Limpiar el texto antes de empezar

        textoTMP.gameObject.SetActive(true);
        canvasGroup.alpha = 1;

        // Escribir el mensaje letra por letra
        foreach (char letra in mensaje)
        {
            textoTMP.text += letra;
            yield return new WaitForSeconds(velocidadEscritura);
        }

        yield return new WaitForSeconds(tiempoEsperaAntesDesvanecer);

        // Fade out
        float t = 0f;
        while (t < duracionDesvanecer)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / duracionDesvanecer);
            t += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        textoTMP.gameObject.SetActive(false);

        Destroy(gameObject); // Elimina el trigger para que no se active de nuevo
    }
}
