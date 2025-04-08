using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoManager : MonoBehaviour
{
    [Header("Referencias del diálogo")]
    public CanvasGroup canvasGroup;
    public Image imagenBocadillo;
    public TMP_Text textoNombre;
    public TMP_Text textoDialogo;
    public Transform jugador;

    [Header("Movimiento del personaje")]
    public MonoBehaviour scriptMovimientoKaito;

    [Header("Parámetros")]
    public float distanciaInteraccion = 3f;
    public float duracionFade = 0.5f;
    public float velocidadTexto = 0.05f;

    private bool enRango = false;
    private bool dialogoActivo = false;
    private bool puedeCerrar = false;
    private bool textoEscribiendose = false;
    private bool dialogoOcultoPorPause = false;
    private Coroutine escribiendoTexto;

    private string textoCompleto;
    private string nombreCompleto;

    void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
        textoNombre.gameObject.SetActive(false);
        textoDialogo.gameObject.SetActive(false);

        textoCompleto = textoDialogo.text;
        nombreCompleto = textoNombre.text;

        textoNombre.text = "";
        textoDialogo.text = "";
    }

    void Update()
    {
        enRango = Vector3.Distance(transform.position, jugador.position) <= distanciaInteraccion;

        // Activar diálogo
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogoActivo)
                IniciarDialogo();
            else if (textoEscribiendose)
                MostrarTextoInmediatamente();
            else if (puedeCerrar)
                CerrarDialogo();
        }

        // Pausar juego con ESC
        if (Input.GetKeyDown(KeyCode.Escape) && dialogoActivo)
        {
            if (!dialogoOcultoPorPause)
            {
                OcultarPorPausa();
            }
            else
            {
                ReanudarTrasPausa();
            }
        }
    }

    void IniciarDialogo()
    {
        dialogoActivo = true;
        puedeCerrar = false;
        dialogoOcultoPorPause = false;

        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(true);
        textoNombre.gameObject.SetActive(false);
        textoDialogo.gameObject.SetActive(false);

        textoNombre.text = "";
        textoDialogo.text = "";

        if (scriptMovimientoKaito != null)
            scriptMovimientoKaito.enabled = false;

        Time.timeScale = 0f;
        StartCoroutine(AnimarDialogo());
    }

    IEnumerator AnimarDialogo()
    {
        float t = 0f;
        while (t < duracionFade)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / duracionFade);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        yield return new WaitForSecondsRealtime(0.5f);
        textoNombre.text = nombreCompleto;
        textoNombre.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(0.3f);
        textoDialogo.gameObject.SetActive(true);
        escribiendoTexto = StartCoroutine(EscribirTextoMaquina(textoCompleto));
    }

    IEnumerator EscribirTextoMaquina(string texto)
    {
        textoDialogo.text = "";
        textoEscribiendose = true;
        foreach (char letra in texto)
        {
            textoDialogo.text += letra;
            yield return new WaitForSecondsRealtime(velocidadTexto);
        }

        textoEscribiendose = false;
        puedeCerrar = true;
    }

    void MostrarTextoInmediatamente()
    {
        if (escribiendoTexto != null)
            StopCoroutine(escribiendoTexto);

        textoDialogo.text = textoCompleto;
        textoEscribiendose = false;
        puedeCerrar = true;
    }

    void CerrarDialogo()
    {
        if (escribiendoTexto != null)
            StopCoroutine(escribiendoTexto);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float t = 0f;
        while (t < duracionFade)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / duracionFade);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);

        textoNombre.text = "";
        textoDialogo.text = "";
        textoNombre.gameObject.SetActive(false);
        textoDialogo.gameObject.SetActive(false);

        if (scriptMovimientoKaito != null)
            scriptMovimientoKaito.enabled = true;

        Time.timeScale = 1f;
        dialogoActivo = false;
        puedeCerrar = false;
    }

    void OcultarPorPausa()
    {
        canvasGroup.gameObject.SetActive(false);
        dialogoOcultoPorPause = true;
    }

    void ReanudarTrasPausa()
    {
        canvasGroup.gameObject.SetActive(true);
        dialogoOcultoPorPause = false;
    }
}