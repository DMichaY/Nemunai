using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorCanvasFinal : MonoBehaviour
{
    public Button botonSi;
    public Button botonNo;

    private string escenaDestino;
    private GameObject canvasAsociado;

    private void Start()
    {
        // Por si los botones no están asignados desde el inspector
        if (botonSi != null)
            botonSi.onClick.AddListener(PulsarSi);

        if (botonNo != null)
            botonNo.onClick.AddListener(PulsarNo);
    }

    public void Configurar(string escena, GameObject canvas)
    {
        escenaDestino = escena;
        canvasAsociado = canvas;
    }

    public void PulsarSi()
    {
        if (!string.IsNullOrEmpty(escenaDestino))
        {
            SceneManager.LoadScene(escenaDestino);
        }
    }

    public void PulsarNo()
    {
        if (canvasAsociado != null)
        {
            canvasAsociado.SetActive(false);
        }
    }
}
