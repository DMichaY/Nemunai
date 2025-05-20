using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;

public class MenuPausa : MonoBehaviour
{
    public bool pausado = false;

    [Header("Paneles principales")]
    public GameObject panelPausa;
    public GameObject apartadoPausa;
    public GameObject apartadoOpciones;
    public GameObject apartadoConfirmarVolver;
    public GameObject apartadoConfirmarSalir;
    public GameObject opcionesVideo;
    public GameObject opcionesSonido;
    public GameObject opcionesControles;

    [Header("Botones de navegación")]
    public Button btnContinuar, btnOpciones, btnVolverMenu, btnSalir;
    public Button btnMenuSi, btnMenuNo, btnSalirSi, btnSalirNo;
    public Button btnAtras, btnVideo, btnSonido, btnControles;
    public Button btnAtrasVideo, btnAtrasSonido, btnAtrasControles;
    public Button btnTeclado, btnMando;

    [Header("Pantalla")]
    public Button btnPantallaIzq, btnPantallaDer;
    public TextMeshProUGUI txtPantallaCompleta, txtVentana;

    [Header("Resolución y calidad")]
    public Button btnCalidadIzq, btnCalidadDer;
    public TextMeshProUGUI txtGraficosAltos, txtGraficosBajos;

    [Header("Brillo")]
    public Button btnBrilloIzq, btnBrilloDer;
    public TextMeshProUGUI txtBrillo;
    public Image filtroBrillo;
    private float brillo = 1f;

    [Header("Sonido")]
    public Slider sliderGeneral, sliderMusica, sliderSFX;

    [Header("Audio")]
    public AudioClip sonidoHover;
    public AudioClip sonidoClick;
    public AudioMixer mezcladorPrincipal;
    public AudioMixerGroup grupoMusica;
    public AudioMixerGroup grupoSFX;
    private AudioSource fuenteSFX;

    [Header("Controles")]
    public GameObject imagenControlesTeclado, imagenControlesMando;

    private int pantallaIndex; // 0 = Pantalla Completa, 1 = Ventana Sin Bordes
    private int calidadIndex;  // 0 = Bajo, 1 = Alto

    void Awake()
    {
        GameObject objSFX = new GameObject("FuenteAudioSFX");
        objSFX.transform.parent = transform;
        fuenteSFX = objSFX.AddComponent<AudioSource>();
        fuenteSFX.outputAudioMixerGroup = grupoSFX;

        foreach (Button b in FindObjectsOfType<Button>())
            AsignarEventosAudio(b);
    }

    void Start()
    {
        CargarConfiguraciones();

        // Botones principales
        btnContinuar.onClick.AddListener(Continuar);
        btnOpciones.onClick.AddListener(() => CambiarApartado(apartadoPausa, apartadoOpciones));
        btnVolverMenu.onClick.AddListener(() => CambiarApartado(apartadoPausa, apartadoConfirmarVolver));
        btnSalir.onClick.AddListener(() => CambiarApartado(apartadoPausa, apartadoConfirmarSalir));

        btnMenuSi.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MenuPrincipal");
        });
        btnMenuNo.onClick.AddListener(() => CambiarApartado(apartadoConfirmarVolver, apartadoPausa));
        btnSalirSi.onClick.AddListener(SalirDelJuego);
        btnSalirNo.onClick.AddListener(() => CambiarApartado(apartadoConfirmarSalir, apartadoPausa));

        btnAtras.onClick.AddListener(() => CambiarApartado(apartadoOpciones, apartadoPausa));
        btnVideo.onClick.AddListener(() => CambiarApartado(apartadoOpciones, opcionesVideo));
        btnSonido.onClick.AddListener(() => CambiarApartado(apartadoOpciones, opcionesSonido));
        btnControles.onClick.AddListener(() => CambiarApartado(apartadoOpciones, opcionesControles));

        btnAtrasVideo.onClick.AddListener(() => CambiarApartado(opcionesVideo, apartadoOpciones));
        btnAtrasSonido.onClick.AddListener(() => CambiarApartado(opcionesSonido, apartadoOpciones));
        btnAtrasControles.onClick.AddListener(() => CambiarApartado(opcionesControles, apartadoOpciones));

        // Video
        btnPantallaIzq.onClick.AddListener(() => CambiarPantalla(-1));
        btnPantallaDer.onClick.AddListener(() => CambiarPantalla(1));
        btnCalidadIzq.onClick.AddListener(() => CambiarCalidad(-1));
        btnCalidadDer.onClick.AddListener(() => CambiarCalidad(1));
        btnBrilloIzq.onClick.AddListener(() => CambiarBrillo(-0.1f));
        btnBrilloDer.onClick.AddListener(() => CambiarBrillo(0.1f));

        // Sonido
        sliderGeneral.onValueChanged.AddListener(v => mezcladorPrincipal.SetFloat("VolGeneral", Mathf.Log10(v) * 20));
        sliderMusica.onValueChanged.AddListener(v => mezcladorPrincipal.SetFloat("VolMusica", Mathf.Log10(v) * 20));
        sliderSFX.onValueChanged.AddListener(v => mezcladorPrincipal.SetFloat("VolSFX", Mathf.Log10(v) * 20));

        sliderGeneral.value = PlayerPrefs.GetFloat("SonidoGeneral", 1f);
        sliderMusica.value = PlayerPrefs.GetFloat("SonidoMusica", 1f);
        sliderSFX.value = PlayerPrefs.GetFloat("SonidoSFX", 1f);

        // Controles
        btnTeclado.onClick.AddListener(() => SeleccionarControles(true));
        btnMando.onClick.AddListener(() => SeleccionarControles(false));

        panelPausa.SetActive(false);
        Time.timeScale = 1;
    }

    
    void CargarConfiguraciones()
    {
        brillo = PlayerPrefs.GetFloat("Brillo", 0.5f); // 50% por defecto
        ActualizarFiltroBrillo();

        pantallaIndex = PlayerPrefs.GetInt("Pantalla", 0);
        CambiarPantalla(0);

        calidadIndex = PlayerPrefs.GetInt("Calidad", 1);
        CambiarCalidad(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado) Continuar();
            else Pausar();
        }
    }

    void Pausar()
    {
        pausado = true;
        Time.timeScale = 0;
        panelPausa.SetActive(true);
        ActivarSoloEsteApartado(apartadoPausa);
    }

    void Continuar()
    {
        pausado = false;
        Time.timeScale = 1;
        panelPausa.SetActive(false);
    }

    void CambiarApartado(GameObject de, GameObject a)
    {
        de.SetActive(false);
        a.SetActive(true);
    }

    void ActivarSoloEsteApartado(GameObject a)
    {
        apartadoPausa.SetActive(false);
        apartadoOpciones.SetActive(false);
        apartadoConfirmarVolver.SetActive(false);
        apartadoConfirmarSalir.SetActive(false);
        opcionesVideo.SetActive(false);
        opcionesSonido.SetActive(false);
        opcionesControles.SetActive(false);
        a.SetActive(true);
    }

    void SalirDelJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void CambiarPantalla(int cambio)
    {
        pantallaIndex = (pantallaIndex + cambio + 2) % 2;
        PlayerPrefs.SetInt("Pantalla", pantallaIndex);

        txtPantallaCompleta.gameObject.SetActive(pantallaIndex == 0);
        txtVentana.gameObject.SetActive(pantallaIndex == 1);

        Screen.fullScreenMode = (pantallaIndex == 0) ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.FullScreenWindow;
    }

    void CambiarCalidad(int cambio)
    {
        calidadIndex = Mathf.Clamp(calidadIndex + cambio, 0, 1);
        PlayerPrefs.SetInt("Calidad", calidadIndex);

        int calidadUnity = (calidadIndex == 0) ? 0 : QualitySettings.names.Length - 1;
        QualitySettings.SetQualityLevel(calidadUnity);

        txtGraficosAltos.gameObject.SetActive(calidadIndex == 1);
        txtGraficosBajos.gameObject.SetActive(calidadIndex == 0);
    }

    void CambiarBrillo(float delta)
    {
        brillo = Mathf.Clamp01(brillo + delta);
        PlayerPrefs.SetFloat("Brillo", brillo);
        ActualizarFiltroBrillo();
    }

    void ActualizarFiltroBrillo()
    {
        Color finalColor;
        float alpha;

        if (brillo < 0.5f)
        {
            // De negro con alpha 200 → alpha 0
            alpha = Mathf.Lerp(200f / 255f, 0f, brillo / 0.5f);
            finalColor = new Color(0f, 0f, 0f, alpha);
        }
        else
        {
            // De alpha 0 → blanco con alpha 100
            alpha = Mathf.Lerp(0f, 100f / 255f, (brillo - 0.5f) / 0.5f);
            finalColor = new Color(1f, 1f, 1f, alpha);
        }

        filtroBrillo.color = finalColor;
        txtBrillo.text = Mathf.RoundToInt(brillo * 100f) + "%";
    }

    void SeleccionarControles(bool teclado)
    {
        imagenControlesTeclado.SetActive(teclado);
        imagenControlesMando.SetActive(!teclado);
    }

    void AsignarEventosAudio(Button boton)
    {
        EventTrigger trigger = boton.gameObject.GetComponent<EventTrigger>() ?? boton.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry hover = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        hover.callback.AddListener((e) => ReproducirSFX(sonidoHover));
        trigger.triggers.Add(hover);

        boton.onClick.AddListener(() => ReproducirSFX(sonidoClick));
    }

    void ReproducirSFX(AudioClip clip)
    {
        if (clip != null && fuenteSFX != null)
            fuenteSFX.PlayOneShot(clip);
    }
}
