using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ConfirmarFinalTrasCinematica : MonoBehaviour
{
    public VideoPlayer videoPlayer;              // Asigna el VideoPlayer que reproduce el video
    public GameObject panelConfirmar;            // El panel del canvas que se activará

    public Button botonFinalA;                   // Botón que carga la escena A
    public Button botonFinalB;                   // Botón que carga la escena B

    public string escenaFinalA;                  // Nombre de la escena para el botón A
    public string escenaFinalB;                  // Nombre de la escena para el botón B

    private bool yaActivado = false;

    void Start()
    {
        if (panelConfirmar != null)
            panelConfirmar.SetActive(false); // Asegúrate de que esté oculto al inicio

        if (videoPlayer != null)
            videoPlayer.loopPointReached += AlTerminarVideo; // Se llama cuando el video termina

        if (botonFinalA != null)
            botonFinalA.onClick.AddListener(() => SceneManager.LoadScene(escenaFinalA));

        if (botonFinalB != null)
            botonFinalB.onClick.AddListener(() => SceneManager.LoadScene(escenaFinalB));
    }

    void AlTerminarVideo(VideoPlayer vp)
    {
        if (!yaActivado)
        {
            panelConfirmar.SetActive(true);
            yaActivado = true;
        }
    }

    private void OnDestroy()
    {
        if (videoPlayer != null)
            videoPlayer.loopPointReached -= AlTerminarVideo;
    }
}