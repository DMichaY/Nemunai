using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    [Header("Botones y sus imágenes a ocultar")]
    public Button btnContinuar, btnOpciones, btnVolverMenu, btnSalir;
    public Button btnMenuSi, btnMenuNo, btnSalirSi, btnSalirNo;
    public Button btnAtras, btnVideo, btnSonido, btnControles;
    public GameObject imgBtnContinuar, imgBtnOpciones, imgBtnVolverMenu, imgBtnSalir;
    public GameObject imgBtnMenuSi, imgBtnMenuNo, imgBtnSalirSi, imgBtnSalirNo;
    public GameObject imgBtnAtras, imgBtnVideo, imgBtnSonido, imgBtnControles;

    [Header("Opciones Video - Botón atrás")]
    public Button btnAtrasVideo;
    public GameObject imgBtnAtrasVideo;

    [Header("Opciones Video - Tipo de pantalla")]
    public Button btnPantallaIzq, btnPantallaDer;
    public GameObject[] pantallas;

    [Header("Opciones Video - Resoluciones")]
    public Button btnResolucionIzq, btnResolucionDer;
    public GameObject[] resoluciones;

    [Header("Opciones Video - Calidad gráfica")]
    public Button btnCalidadIzq, btnCalidadDer;
    public GameObject[] calidades;

    private int pantallaIndex = 0;
    private int resolucionIndex = 1;
    private int calidadIndex = 2;

    [Header("Opciones Sonido")]
    public Button btnAtrasSonido;
    public GameObject imgBtnAtrasSonido;
    public Slider sliderGeneral, sliderMusica, sliderSFX;

    [Header("Opciones Controles")]
    public Button btnAtrasControles, btnTeclado, btnMando;
    public GameObject imgBtnAtrasControles, imgBtnTeclado, imgBtnMando;
    public GameObject imagenControlesTeclado, imagenControlesMando;

    void Start()
    {
        panelPausa.SetActive(false);
        Time.timeScale = 1;

        btnContinuar.onClick.AddListener(Continuar);
        btnOpciones.onClick.AddListener(AbrirOpciones);
        btnVolverMenu.onClick.AddListener(() => AlternarApartado(apartadoPausa, apartadoConfirmarVolver));
        btnSalir.onClick.AddListener(() => AlternarApartado(apartadoPausa, apartadoConfirmarSalir));

        btnMenuSi.onClick.AddListener(VolverAlMenu);
        btnMenuNo.onClick.AddListener(() => AlternarApartado(apartadoConfirmarVolver, apartadoPausa));
        btnSalirSi.onClick.AddListener(SalirDelJuego);
        btnSalirNo.onClick.AddListener(() => AlternarApartado(apartadoConfirmarSalir, apartadoPausa));

        btnAtras.onClick.AddListener(() => AlternarApartado(apartadoOpciones, apartadoPausa));
        btnVideo.onClick.AddListener(() => AlternarApartado(apartadoOpciones, opcionesVideo));
        btnSonido.onClick.AddListener(() => AlternarApartado(apartadoOpciones, opcionesSonido));
        btnControles.onClick.AddListener(() => AlternarApartado(apartadoOpciones, opcionesControles));

        btnAtrasVideo.onClick.AddListener(() => AlternarApartado(opcionesVideo, apartadoOpciones));
        btnAtrasSonido.onClick.AddListener(() => AlternarApartado(opcionesSonido, apartadoOpciones));
        btnAtrasControles.onClick.AddListener(() => AlternarApartado(opcionesControles, apartadoOpciones));

        btnPantallaIzq.onClick.AddListener(() => CambiarIndice(ref pantallaIndex, -1, pantallas.Length, ActualizarPantalla));
        btnPantallaDer.onClick.AddListener(() => CambiarIndice(ref pantallaIndex, 1, pantallas.Length, ActualizarPantalla));
        btnResolucionIzq.onClick.AddListener(() => CambiarIndice(ref resolucionIndex, -1, resoluciones.Length, ActualizarResolucion));
        btnResolucionDer.onClick.AddListener(() => CambiarIndice(ref resolucionIndex, 1, resoluciones.Length, ActualizarResolucion));
        btnCalidadIzq.onClick.AddListener(() => CambiarIndice(ref calidadIndex, -1, calidades.Length, ActualizarCalidad));
        btnCalidadDer.onClick.AddListener(() => CambiarIndice(ref calidadIndex, 1, calidades.Length, ActualizarCalidad));

        sliderGeneral.onValueChanged.AddListener((v) => PlayerPrefs.SetFloat("SonidoGeneral", v));
        sliderMusica.onValueChanged.AddListener((v) => PlayerPrefs.SetFloat("SonidoMusica", v));
        sliderSFX.onValueChanged.AddListener((v) => PlayerPrefs.SetFloat("SonidoSFX", v));

        sliderGeneral.value = PlayerPrefs.GetFloat("SonidoGeneral", 1);
        sliderMusica.value = PlayerPrefs.GetFloat("SonidoMusica", 1);
        sliderSFX.value = PlayerPrefs.GetFloat("SonidoSFX", 1);

        btnTeclado.onClick.AddListener(() => SeleccionarControles(true));
        btnMando.onClick.AddListener(() => SeleccionarControles(false));

        AsignarEventosHover();

        SeleccionarControles(true);
        ActualizarPantalla();
        ActualizarResolucion();
        ActualizarCalidad();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausado) PausarJuego();
            else Continuar();
        }
    }

    void PausarJuego()
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

    void AbrirOpciones() => AlternarApartado(apartadoPausa, apartadoOpciones);

    void VolverAlMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuPrincipal");
    }

    void SalirDelJuego()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void AlternarApartado(GameObject de, GameObject a)
    {
        de.SetActive(false);
        a.SetActive(true);
    }

    void ActivarSoloEsteApartado(GameObject apartado)
    {
        apartadoPausa.SetActive(false);
        apartadoOpciones.SetActive(false);
        apartadoConfirmarVolver.SetActive(false);
        apartadoConfirmarSalir.SetActive(false);
        opcionesVideo.SetActive(false);
        opcionesSonido.SetActive(false);
        opcionesControles.SetActive(false);
        apartado.SetActive(true);
    }

    void AsignarEventosHover()
    {
        AsignarHover(btnContinuar, imgBtnContinuar);
        AsignarHover(btnOpciones, imgBtnOpciones);
        AsignarHover(btnVolverMenu, imgBtnVolverMenu);
        AsignarHover(btnSalir, imgBtnSalir);
        AsignarHover(btnMenuSi, imgBtnMenuSi);
        AsignarHover(btnMenuNo, imgBtnMenuNo);
        AsignarHover(btnSalirSi, imgBtnSalirSi);
        AsignarHover(btnSalirNo, imgBtnSalirNo);
        AsignarHover(btnAtras, imgBtnAtras);
        AsignarHover(btnVideo, imgBtnVideo);
        AsignarHover(btnSonido, imgBtnSonido);
        AsignarHover(btnControles, imgBtnControles);
        AsignarHover(btnAtrasVideo, imgBtnAtrasVideo);
        AsignarHover(btnAtrasSonido, imgBtnAtrasSonido);
        AsignarHover(btnAtrasControles, imgBtnAtrasControles);
        AsignarHover(btnTeclado, imgBtnTeclado);
        AsignarHover(btnMando, imgBtnMando);
    }

    void AsignarHover(Button btn, GameObject imagen)
    {
        if (btn == null || imagen == null) return;
        EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>() ?? btn.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        entryEnter.callback.AddListener((e) => imagen.SetActive(false));
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        entryExit.callback.AddListener((e) => imagen.SetActive(true));
        trigger.triggers.Add(entryExit);
    }

    void CambiarIndice(ref int indice, int cambio, int max, System.Action actualizar)
    {
        indice = (indice + cambio + max) % max;
        actualizar.Invoke();
    }

    void ActualizarPantalla()
    {
        for (int i = 0; i < pantallas.Length; i++)
            pantallas[i].SetActive(i == pantallaIndex);
    }

    void ActualizarResolucion()
    {
        for (int i = 0; i < resoluciones.Length; i++)
            resoluciones[i].SetActive(i == resolucionIndex);
    }

    void ActualizarCalidad()
    {
        for (int i = 0; i < calidades.Length; i++)
            calidades[i].SetActive(i == calidadIndex);
    }

    void SeleccionarControles(bool teclado)
    {
        imagenControlesTeclado.SetActive(teclado);
        imagenControlesMando.SetActive(!teclado);
    }
}
