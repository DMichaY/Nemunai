using UnityEngine;
using UnityEngine.Video;

public class ControlIntroVideo : MonoBehaviour
{
    private VideoPlayer reproductorVideo;

    void Awake()
    {
        // Bloquear la entrada del jugador al iniciar
        GestorEntrada.BloquearEntrada();
    }

    void Start()
    {
        reproductorVideo = GetComponent<VideoPlayer>();

        if (reproductorVideo == null)
        {
            Debug.LogError("No se encontró un componente VideoPlayer en este objeto.");
            return;
        }

        // Opcional: desactiva el audio si no hace falta
        reproductorVideo.audioOutputMode = VideoAudioOutputMode.None;

        // Reproduce el vídeo automáticamente
        reproductorVideo.Play();

        // Evento al terminar el vídeo
        reproductorVideo.loopPointReached += AlTerminarVideo;
    }

    void AlTerminarVideo(VideoPlayer vp)
    {
        // Desbloquear controles
        GestorEntrada.DesbloquearEntrada();

        // Detener el vídeo
        reproductorVideo.Stop();

        // Desactivar el objeto del vídeo para que desaparezca visualmente
        gameObject.SetActive(false);
    }
}
