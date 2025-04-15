using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoManager : Interactable
{
    public CanvasGroup canvasGroup;
    public Image imagenBocadillo;
    public TMP_Text textoNombre;
    public TMP_Text textoDialogo;
    public Transform jugador;
    public MonoBehaviour scriptMovimientoKaito;

    public float distanciaInteraccion = 3f;
    public float duracionFade = 0.5f;
    public float velocidadTexto = 0.05f;
    public float velocidadRotacion = 5f;

    private bool dialogoActivo = false;
    private bool puedeCerrar = false;
    private bool textoEscribiendose = false;
    private bool dialogoOcultoPorPause = false;
    private bool estaPausado = false;

    private Coroutine escribiendoTexto;
    private Coroutine rotacionCoroutine;

    private Vector3 rotacionInicial;

    void Start()
    {
        rotacionInicial = transform.eulerAngles;

        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
        textoNombre.gameObject.SetActive(false);
        textoDialogo.gameObject.SetActive(false);
    }

    public override void Interact()
    {
        if (!dialogoActivo && !dialogoOcultoPorPause)
            StartCoroutine(IniciarDialogo());
        else if (dialogoActivo && textoEscribiendose)
            MostrarTextoInmediatamente();
        else if (dialogoActivo && puedeCerrar)
            CerrarDialogo();

        if (Input.GetKeyDown(KeyCode.Escape) && dialogoActivo)
        {
            if (!dialogoOcultoPorPause)
                OcultarPorPausa();
            else
                ReanudarTrasPausa();
        }
    }

    IEnumerator IniciarDialogo()
    {
        dialogoActivo = true;
        puedeCerrar = false;
        dialogoOcultoPorPause = false;
        estaPausado = false;

        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(true);
        textoNombre.gameObject.SetActive(false);
        textoDialogo.gameObject.SetActive(false);

        if (scriptMovimientoKaito != null)
            scriptMovimientoKaito.enabled = false;

        if (rotacionCoroutine != null)
            StopCoroutine(rotacionCoroutine);
        rotacionCoroutine = StartCoroutine(RotarHaciaJugador());

        Time.timeScale = 0f;
        StartCoroutine(AnimarDialogo());
        yield return null;
    }

    IEnumerator RotarHaciaJugador()
    {
        Vector3 direccion = (jugador.position - transform.position).normalized;
        direccion.y = 0f;
        Quaternion rotObjetivo = Quaternion.LookRotation(direccion);
        while (Quaternion.Angle(transform.rotation, rotObjetivo) > 0.5f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotObjetivo, Time.unscaledDeltaTime * velocidadRotacion);
            yield return null;
        }
        transform.rotation = rotObjetivo;
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


        // Obtener texto actualizado
        string nombreCompleto = textoNombre.text;

        textoNombre.text = "";
        yield return new WaitForSecondsRealtime(0.5f);
        textoNombre.text = nombreCompleto;
        textoNombre.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(0.3f);
        textoDialogo.gameObject.SetActive(true);

        string textoCompleto = textoDialogo.text;
        textoDialogo.text = "";
        escribiendoTexto = StartCoroutine(EscribirTextoMaquina(textoCompleto));
    }

    IEnumerator EscribirTextoMaquina(string texto)
    {
        textoEscribiendose = true;
        foreach (char letra in texto)
        {
            if (dialogoOcultoPorPause) yield break;
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

        textoDialogo.text = textoDialogo.text; // ya actualizado
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
        textoNombre.gameObject.SetActive(false);
        textoDialogo.gameObject.SetActive(false);

        if (scriptMovimientoKaito != null)
            scriptMovimientoKaito.enabled = true;

        Time.timeScale = 1f;
        dialogoActivo = false;
        puedeCerrar = false;

        if (rotacionCoroutine != null)
            StopCoroutine(rotacionCoroutine);
        StartCoroutine(RotarAHaciaInicial());
    }

    void OcultarPorPausa()
    {
        if (dialogoActivo)
        {
            canvasGroup.gameObject.SetActive(false);
            dialogoOcultoPorPause = true;
            estaPausado = true;
        }
    }

    void ReanudarTrasPausa()
    {
        if (dialogoActivo && estaPausado)
        {
            canvasGroup.gameObject.SetActive(true);
            dialogoOcultoPorPause = false;
            estaPausado = false;

            if (textoEscribiendose)
            {
                string textoRestante = textoDialogo.text;
                escribiendoTexto = StartCoroutine(EscribirTextoMaquina(textoRestante.Substring(textoDialogo.text.Length)));
            }
        }
    }

    IEnumerator RotarAHaciaInicial()
    {
        Quaternion rotObjetivo = Quaternion.Euler(rotacionInicial);
        while (Quaternion.Angle(transform.rotation, rotObjetivo) > 0.5f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotObjetivo, Time.unscaledDeltaTime * velocidadRotacion);
            yield return null;
        }
        transform.rotation = rotObjetivo;
    }
}
