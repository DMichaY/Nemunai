using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class AcelerarVideoConTecla : MonoBehaviour
{
    [Header("Video y configuración")]
    public VideoPlayer pantallaVideo;
    public KeyCode tecla = KeyCode.E;

    [Header("Textos")]
    public CanvasGroup textoInicial;
    public GameObject textoEActivo;

    [Header("Ajustes de texto")]
    public float tiempoVisibleTexto = 5f;
    public float velocidadFade = 5f;

    private float tiempoPulsado = 0f;
    private Coroutine fadeTextoInicial;
    private Coroutine temporizadorOcultarTexto;
    private bool videoTerminado = false;

    GameObject manager;
    MenuPausa pausador;

    void Start()
    {
        manager = GameObject.Find("GameManager");
        pausador = manager.GetComponent<MenuPausa>();

        if (textoInicial != null) textoInicial.alpha = 0f;
        if (textoEActivo != null) textoEActivo.SetActive(false);

        MostrarTextoInicial();

        if (pantallaVideo != null)
            pantallaVideo.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        if (videoTerminado) return;
        if (!pausador.pausado)
        {
            if (Input.GetKey(tecla))
            {
                tiempoPulsado += Time.unscaledDeltaTime;

                pantallaVideo.playbackSpeed = tiempoPulsado >= 5f ? 4f : 2f;
                Time.timeScale = tiempoPulsado >= 5f ? 4f : 2f;

                textoEActivo.SetActive(true);

                if (textoInicial.gameObject.activeSelf)
                {
                    textoInicial.gameObject.SetActive(false);
                    StopFade(ref fadeTextoInicial);
                    textoInicial.alpha = 0f;
                }

                if (temporizadorOcultarTexto != null)
                {
                    StopCoroutine(temporizadorOcultarTexto);
                    temporizadorOcultarTexto = null;
                }
            }
            else
            {
                if (tiempoPulsado > 0f)
                {
                    textoEActivo.SetActive(false);
                    textoInicial.gameObject.SetActive(true);
                    MostrarTextoInicial();
                }

                tiempoPulsado = 0f;
                pantallaVideo.playbackSpeed = 1f;
                Time.timeScale = 1;
            }
        }
    }

    void MostrarTextoInicial()
    {
        StopFade(ref fadeTextoInicial);
        fadeTextoInicial = StartCoroutine(FadeCanvasGroup(textoInicial, 1f));

        if (temporizadorOcultarTexto != null)
            StopCoroutine(temporizadorOcultarTexto);

        temporizadorOcultarTexto = StartCoroutine(TemporizadorOcultarTexto());
    }

    IEnumerator TemporizadorOcultarTexto()
    {
        yield return new WaitForSeconds(tiempoVisibleTexto);

        StopFade(ref fadeTextoInicial);
        fadeTextoInicial = StartCoroutine(FadeCanvasGroup(textoInicial, 0f));
    }

    IEnumerator FadeCanvasGroup(CanvasGroup group, float targetAlpha)
    {
        if (group == null) yield break;

        float t = 0f;
        float startAlpha = group.alpha;

        while (Mathf.Abs(group.alpha - targetAlpha) > 0.01f)
        {
            t += Time.unscaledDeltaTime * velocidadFade;
            group.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        group.alpha = targetAlpha;

        if (targetAlpha == 0f)
            group.gameObject.SetActive(false);
    }

    void StopFade(ref Coroutine fade)
    {
        if (fade != null)
        {
            StopCoroutine(fade);
            fade = null;
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        videoTerminado = true;
        Time.timeScale = 1;

        StopFade(ref fadeTextoInicial);
        StartCoroutine(FadeCanvasGroup(textoInicial, 0f));

        textoEActivo.SetActive(false);
    }
}
