using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoCinematica : MonoBehaviour
{
    [Header("Referencias")]
    public VideoPlayer videoPlayer;               // Componente VideoPlayer
    public GameObject pantallaVideo;              // UI que muestra el v�deo (con RawImage)
    public GameObject juegoCompleto;              // Padre del gameplay (personaje, c�mara, etc.)
    public Camera camaraReferencia;               // C�mara que, si se desactiva, cierra la carta

    [Header("Gameplay")]
    public float segundosAntesDeActivarGameplay = 1f;

    private bool videoPausado = false;

    public Image pantallaNegra;

    void Start()
    {
        juegoCompleto.SetActive(false);
        videoPlayer.loopPointReached += AlTerminarVideo;
        videoPlayer.Play();

        StartCoroutine(ActivarJuegoAntesDeFin());
    }

    void Update()
    {
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

    // Funci�n p�blica para reanudar el v�deo y los controles
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

        if (pantallaNegra != null)

            pantallaNegra.CrossFadeAlpha(0, 2, false);
        else
            Debug.LogWarning("No hay pantalla negra!");
    }

    void AlTerminarVideo(VideoPlayer vp)
    {
        pantallaVideo.SetActive(false);

    }
}