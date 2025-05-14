using UnityEngine;
using UnityEngine.Video;

public class AcelerarVideoConTecla : MonoBehaviour
{
    [Header("Asigna aquí el componente VideoPlayer")]
    public VideoPlayer pantallaVideo;

    [Header("Tecla para acelerar el vídeo")]
    public KeyCode tecla = KeyCode.E;

    private float tiempoPulsado = 0f;

    void Update()
    {
        if (Input.GetKey(tecla))
        {
            tiempoPulsado += Time.deltaTime;

            if (tiempoPulsado >= 5f)
            {
                pantallaVideo.playbackSpeed = 4f;
            }
            else
            {
                pantallaVideo.playbackSpeed = 2f;
            }
        }
        else
        {
            tiempoPulsado = 0f;
            pantallaVideo.playbackSpeed = 1f;
        }
    }
}
