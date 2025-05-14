using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoIntroController : MonoBehaviour
{
    [Header("Referencias")]
    public VideoPlayer videoPlayer;               // Componente VideoPlayer
    public GameObject pantallaVideo;              // UI que muestra el vídeo (con RawImage)
    public GameObject carta;                      // Objeto UI que se anima (RectTransform)
    public GameObject juegoCompleto;              // Padre del gameplay (personaje, cámara, etc.)
    public Camera camaraReferencia;               // Cámara que, si se desactiva, cierra la carta

    [Header("Animación de carta")]
    public float desplazamientoY = 300f;
    public float duracionAnimEntrada = 1f;
    public float duracionAnimSalida = 1f;

    [Header("Gameplay")]
    public float segundosAntesDeActivarGameplay = 1f;

    private bool puedeCerrarConE = false;
    private bool cartaAbierta = false;
    private RectTransform cartaRect;
    private Vector2 cartaPosOriginal;

    private bool videoPausado = false;

    void Start()
    {
        juegoCompleto.SetActive(false);
        carta.SetActive(false);

        cartaRect = carta.GetComponent<RectTransform>();
        cartaPosOriginal = cartaRect.anchoredPosition;

        videoPlayer.loopPointReached += AlTerminarVideo;
        videoPlayer.Play();

        StartCoroutine(ActivarJuegoAntesDeFin());
    }

    void Update()
    {
        if (puedeCerrarConE && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CerrarCartaAnimada());
        }

        if (cartaAbierta && camaraReferencia != null && !camaraReferencia.gameObject.activeInHierarchy)
        {
            StartCoroutine(CerrarCartaAnimada());
        }

        if (videoPlayer.isPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            videoPlayer.Pause();
            videoPausado = true;
        }
        else if (videoPausado && Input.GetKeyDown(KeyCode.Escape))
        {
            videoPlayer.Play();
            videoPausado = false;
        }
    }

    // Función pública para reanudar el vídeo y los controles
    public void ReanudarVideo()
    {
        if (videoPausado)
        {
            videoPlayer.Play();
            videoPausado = false;
        }

        // Activar controles
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Activar juego completo
        juegoCompleto.SetActive(true);
    }

    IEnumerator ActivarJuegoAntesDeFin()
    {
        double tiempoActivacion = videoPlayer.clip.length - segundosAntesDeActivarGameplay;

        while (videoPlayer.time < tiempoActivacion)
        {
            yield return null;
        }

        juegoCompleto.SetActive(true);
    }

    void AlTerminarVideo(VideoPlayer vp)
    {
        pantallaVideo.SetActive(false);
        StartCoroutine(MostrarCartaAnimada());
    }

    IEnumerator MostrarCartaAnimada()
    {
        carta.SetActive(true);
        cartaAbierta = true;
        puedeCerrarConE = false;

        Vector2 posicionInicial = cartaPosOriginal - new Vector2(0, desplazamientoY);
        Vector2 posicionFinal = cartaPosOriginal;

        cartaRect.anchoredPosition = posicionInicial;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duracionAnimEntrada;
            cartaRect.anchoredPosition = Vector2.Lerp(posicionInicial, posicionFinal, t);
            yield return null;
        }

        cartaRect.anchoredPosition = posicionFinal;
        puedeCerrarConE = true;
    }

    IEnumerator CerrarCartaAnimada()
    {
        puedeCerrarConE = false;
        cartaAbierta = false;

        Vector2 posicionInicial = cartaRect.anchoredPosition;
        Vector2 posicionFinal = cartaPosOriginal - new Vector2(0, desplazamientoY);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duracionAnimSalida;
            cartaRect.anchoredPosition = Vector2.Lerp(posicionInicial, posicionFinal, t);
            yield return null;
        }

        carta.SetActive(false);

        // Activar controles al cerrar la carta
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
