using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Duración del movimiento de la cámara")]
    public float duracionMovimientoCamara = 1f;

    [Header("Referencias de la cámara y sus puntos objetivo")]
    public Transform camara;
    public Transform camaraMenu;
    public Transform camaraOpciones;
    public Transform camaraCreditos;
    public Transform camaraOpcionSeleccionada; // Compartido por vídeo, sonido, etc.

    [Header("Apartados del Canvas")]
    public GameObject menuPrincipal;
    public GameObject opciones;
    public GameObject creditos;
    public GameObject opcionesVideo;
    public GameObject opcionesSonido;
    public GameObject opcionesIdiomas;
    public GameObject opcionesControles;

    [Header("Imágenes que se desactivan al pasar el cursor")]
    public GameObject imagenJugar;
    public GameObject imagenItchio;
    public GameObject imagenSalir;
    public GameObject imagenOpciones;
    public GameObject imagenCreditos;
    public GameObject imagenVideo;
    public GameObject imagenSonido;
    public GameObject imagenIdiomas;
    public GameObject imagenControles;

    [Header("Escena a cargar al pulsar Jugar")]
    public string nombreEscenaJugar;

    [Header("Enlace de Itchio")]
    public string urlItchio;

    void Start()
    {
        // Aseguramos que la cámara empiece en el punto correcto
        camara.position = camaraMenu.position;
    }

    #region Métodos Públicos - Se asignan desde los botones

    public void Jugar()
    {
        SceneManager.LoadScene(nombreEscenaJugar);
    }

    public void IrAItchio()
    {
        Application.OpenURL(urlItchio);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }

    public void IrAOpciones()
    {
        menuPrincipal.SetActive(false);
        opciones.SetActive(true);
        StartCoroutine(MoverCamara(camaraOpciones.position));
    }

    public void IrACreditos()
    {
        menuPrincipal.SetActive(false);
        creditos.SetActive(true);
        StartCoroutine(MoverCamara(camaraCreditos.position));
    }

    public void IrAVideo()
    {
        opciones.SetActive(false);
        opcionesVideo.SetActive(true);
        StartCoroutine(MoverCamara(camaraOpcionSeleccionada.position));
    }

    public void IrASonido()
    {
        opciones.SetActive(false);
        opcionesSonido.SetActive(true);
        StartCoroutine(MoverCamara(camaraOpcionSeleccionada.position));
    }

    public void IrAIdiomas()
    {
        opciones.SetActive(false);
        opcionesIdiomas.SetActive(true);
        StartCoroutine(MoverCamara(camaraOpcionSeleccionada.position));
    }

    public void IrAControles()
    {
        opciones.SetActive(false);
        opcionesControles.SetActive(true);
        StartCoroutine(MoverCamara(camaraOpcionSeleccionada.position));
    }

    public void VolverAlMenuDesdeOpciones()
    {
        opciones.SetActive(false);
        menuPrincipal.SetActive(true);
        StartCoroutine(MoverCamara(camaraMenu.position));
    }

    public void VolverAOpcionesDesdeSubmenu()
    {
        opcionesVideo.SetActive(false);
        opcionesSonido.SetActive(false);
        opcionesIdiomas.SetActive(false);
        opcionesControles.SetActive(false);
        opciones.SetActive(true);
        StartCoroutine(MoverCamara(camaraOpciones.position));
    }

    public void VolverAlMenuDesdeCreditos()
    {
        creditos.SetActive(false);
        menuPrincipal.SetActive(true);
        StartCoroutine(MoverCamara(camaraMenu.position));
    }

    #endregion

    #region Métodos para eventos del cursor

    public void DesactivarImagen(GameObject imagen)
    {
        if (imagen != null) imagen.SetActive(false);
    }

    public void ActivarImagen(GameObject imagen)
    {
        if (imagen != null) imagen.SetActive(true);
    }

    #endregion

    #region Movimiento suave de cámara

    private System.Collections.IEnumerator MoverCamara(Vector3 destino)
    {
        Vector3 inicio = camara.position;
        float tiempo = 0f;

        while (tiempo < duracionMovimientoCamara)
        {
            camara.position = Vector3.Lerp(inicio, destino, tiempo / duracionMovimientoCamara);
            tiempo += Time.deltaTime;
            yield return null;
        }

        camara.position = destino;
    }

    #endregion
}
