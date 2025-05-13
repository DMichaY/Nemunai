using UnityEngine;
using UnityEngine.Video;

public class ControlVideoConImagen : MonoBehaviour
{
    public VideoPlayer video; // Asigna el VideoPlayer desde el Inspector
    public ControlImagenAnimada controlImagen; // Referencia al otro script

    void Start()
    {
        if (video == null || controlImagen == null)
        {
            Debug.LogError("Faltan referencias en ControlVideoConImagen.");
            return;
        }

        video.loopPointReached += AlTerminarVideo;
    }

    void AlTerminarVideo(VideoPlayer vp)
    {
        controlImagen.MostrarImagen();
    }
}
