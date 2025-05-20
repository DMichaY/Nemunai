using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [Header("Cámara")]
    public Transform mainCamera;
    public float cameraSpeed = 2f;
    private Transform targetCameraPosition;

    [Header("Puntos de Cámara")]
    public Transform menuPosition;
    public Transform opcionesPosition;
    public Transform videoPosition;
    public Transform sonidoPosition;
    public Transform idiomasPosition;
    public Transform controlesPosition;
    public Transform creditosPosition;

    [Header("Botones principales")]
    public Button jugarButton;
    public Button salirButton;
    public Button itchButton;
    public Button creditosButton;

    [Header("Video Opciones")]
    public Button pantallaIzq;
    public Button pantallaDer;
    public GameObject textoPantallaCompleta;
    public GameObject textoVentanaCompleta;

    public Button brilloIzq;
    public Button brilloDer;
    public TMP_Text brilloTexto;

    public Button graficosIzq;
    public Button graficosDer;
    public GameObject textoGraficosAlto;
    public GameObject textoGraficosBajo;

    [Header("Sonido")]
    public Slider volumenGeneralSlider;
    public Slider musicaSlider;
    public Slider sfxSlider;

    [Header("Créditos")]
    public TMP_Text creditosTexto;
    public float velocidadMaquina = 0.05f;
    private string textoOriginalCreditos;

    [Header("Brillo")]
    public Image brilloOverlay;
    [Range(0f, 1f)] public float brillo = 0.5f;

    private int pantallaIndex = 0;
    private int graficosIndex = 1;

    private void Start()
    {
        // Botones principales
        jugarButton.onClick.AddListener(() => SceneManager.LoadScene("Estacion"));
        salirButton.onClick.AddListener(() => Application.Quit());
        itchButton.onClick.AddListener(() => Application.OpenURL("https://dmichay.itch.io"));
        creditosButton.onClick.AddListener(() => MoverCamara(creditosPosition, true));

        // Opciones de vídeo
        pantallaIzq.onClick.AddListener(() => CambiarPantalla(-1));
        pantallaDer.onClick.AddListener(() => CambiarPantalla(1));

        brilloIzq.onClick.AddListener(() => CambiarBrillo(-1));
        brilloDer.onClick.AddListener(() => CambiarBrillo(1));

        graficosIzq.onClick.AddListener(() => CambiarGraficos(0));
        graficosDer.onClick.AddListener(() => CambiarGraficos(1));

        // Sliders sonido
        volumenGeneralSlider.onValueChanged.AddListener((v) => GuardarVolumen("volumen_general", v));
        musicaSlider.onValueChanged.AddListener((v) => GuardarVolumen("musica", v));
        sfxSlider.onValueChanged.AddListener((v) => GuardarVolumen("sfx", v));

        // Cargar preferencias
        brillo = PlayerPrefs.GetFloat("brillo", 0.5f);
        ActualizarBrillo();

        pantallaIndex = PlayerPrefs.GetInt("pantalla", 0);
        graficosIndex = PlayerPrefs.GetInt("graficos", 1);
        ActualizarPantalla();
        ActualizarGraficos();

        volumenGeneralSlider.value = PlayerPrefs.GetFloat("volumen_general", 1);
        musicaSlider.value = PlayerPrefs.GetFloat("musica", 1);
        sfxSlider.value = PlayerPrefs.GetFloat("sfx", 1);

        // Créditos
        if (creditosTexto != null)
        {
            textoOriginalCreditos = creditosTexto.text;
            creditosTexto.text = "";
        }

        // Posición inicial de la cámara
        targetCameraPosition = menuPosition;

        // Hover para desactivar imagen decorativa (hermana del botón)
        foreach (Button b in FindObjectsOfType<Button>())
        {
            EventTriggerListener.Get(b.gameObject).onEnter = () =>
            {
                Transform parent = b.transform.parent;
                foreach (Transform child in parent)
                {
                    if (child != b.transform && child.GetComponent<Image>())
                        child.gameObject.SetActive(false);
                }
            };

            EventTriggerListener.Get(b.gameObject).onExit = () =>
            {
                Transform parent = b.transform.parent;
                foreach (Transform child in parent)
                {
                    if (child != b.transform && child.GetComponent<Image>())
                        child.gameObject.SetActive(true);
                }
            };
        }

        // Desactivar raycast en overlay para no bloquear botones
        if (brilloOverlay != null)
            brilloOverlay.raycastTarget = false;
    }

    private void Update()
    {
        if (mainCamera && targetCameraPosition)
        {
            mainCamera.position = Vector3.Lerp(mainCamera.position, targetCameraPosition.position, Time.deltaTime * cameraSpeed);
            mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, targetCameraPosition.rotation, Time.deltaTime * cameraSpeed);
        }
    }

    public void MoverCamara(Transform destino, bool activarCreditos = false)
    {
        targetCameraPosition = destino;
        if (activarCreditos)
            StartCoroutine(MaquinaEscribirTexto());
    }

    IEnumerator MaquinaEscribirTexto()
    {
        creditosTexto.text = "";
        foreach (char c in textoOriginalCreditos)
        {
            creditosTexto.text += c;
            yield return new WaitForSeconds(velocidadMaquina);
        }
    }

    void CambiarPantalla(int dir)
    {
        pantallaIndex += dir;
        pantallaIndex = Mathf.Clamp(pantallaIndex, 0, 1);
        PlayerPrefs.SetInt("pantalla", pantallaIndex);
        ActualizarPantalla();
    }

    void ActualizarPantalla()
    {
        textoPantallaCompleta.SetActive(pantallaIndex == 0);
        textoVentanaCompleta.SetActive(pantallaIndex == 1);
        Screen.fullScreenMode = (pantallaIndex == 0) ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.FullScreenWindow;
    }

    void CambiarGraficos(int index)
    {
        graficosIndex = index;
        PlayerPrefs.SetInt("graficos", graficosIndex);
        ActualizarGraficos();
    }

    void ActualizarGraficos()
    {
        textoGraficosAlto.SetActive(graficosIndex == 1);
        textoGraficosBajo.SetActive(graficosIndex == 0);
        QualitySettings.SetQualityLevel(graficosIndex);
    }

    public void CambiarBrillo(int direccion)
    {
        brillo += direccion * 0.1f;
        brillo = Mathf.Clamp01(brillo);
        PlayerPrefs.SetFloat("brillo", brillo);
        ActualizarBrillo();
    }

    void ActualizarBrillo()
    {
        Color color = Color.clear;
        float alpha = 0f;

        if (brillo < 0.5f)
        {
            float t = brillo / 0.5f; // 0 a 1
            color = Color.black;
            alpha = Mathf.Lerp(200f, 0f, t); // 200 a 0
        }
        else
        {
            float t = (brillo - 0.5f) / 0.5f; // 0 a 1
            color = Color.white;
            alpha = Mathf.Lerp(0f, 100f, t); // 0 a 100
        }

        color.a = alpha / 255f;
        brilloOverlay.color = color;

        if (brilloTexto != null)
            brilloTexto.text = Mathf.RoundToInt(brillo * 100f) + "%";
    }

    void GuardarVolumen(string clave, float valor)
    {
        PlayerPrefs.SetFloat(clave, valor);
        // Si usas AudioMixer, aquí puedes aplicarlo con SetFloat
    }

    // Funciones públicas para botones "volver atrás"
    public void IrAMenu() => MoverCamara(menuPosition);
    public void IrAOpciones() => MoverCamara(opcionesPosition);
    public void IrAVideo() => MoverCamara(videoPosition);
    public void IrASonido() => MoverCamara(sonidoPosition);
    public void IrAIdiomas() => MoverCamara(idiomasPosition);
    public void IrAControles() => MoverCamara(controlesPosition);
}
