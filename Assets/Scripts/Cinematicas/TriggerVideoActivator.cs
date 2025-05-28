using UnityEngine;
using UnityEngine.Video;

public class TriggerVideoActivator : MonoBehaviour
{
    [Tooltip("El objeto que se activará al entrar en el trigger (debe contener VideoPlayer y RawImage).")]
    public GameObject videoComponent;

    private VideoPlayer videoPlayer;

    private void Start()
    {
        if (videoComponent != null)
        {
            videoPlayer = videoComponent.GetComponent<VideoPlayer>();

            if (videoPlayer != null)
            {
                videoPlayer.loopPointReached += OnVideoFinished;
                videoComponent.SetActive(false); // Asegura que está desactivado al inicio
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && videoPlayer != null)
        {
            videoComponent.SetActive(true);
            videoPlayer.Stop(); // Reinicia desde el inicio
            videoPlayer.Play();
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        videoComponent.SetActive(false);
    }
}
