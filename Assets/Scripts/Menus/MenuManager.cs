using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Cámara")]
    public Transform camara;
    public float velocidadMovimiento = 0.5f;

    [System.Serializable]
    public class BotonConfiguracion
    {
        public string nombre;
        public Button boton;
        public GameObject imagenADesactivar;
        public TipoAccion accion;
        public string datoAccion;
        public Transform destinoCamara;
    }

    [System.Serializable]
    public class BotonSimpleConfiguracion
    {
        public Button boton;
        public GameObject imagenADesactivar;
    }

    public enum TipoAccion
    {
        IrAEscena,
        AbrirURL,
        MoverCamara,
        Salir
    }

    [Header("Botones del Menú Principal")]
    public List<BotonConfiguracion> botonesMenu;

    [Header("Botones del Menú Opciones")]
    public List<BotonConfiguracion> botonesOpciones;

    [Header("Volver Atrás en Subapartados")]
    public BotonConfiguracion botonVolverVideo;
    public BotonConfiguracion botonVolverSonido;
    public BotonConfiguracion botonVolverIdiomas;
    public BotonConfiguracion botonVolverControles;
    public BotonConfiguracion botonVolverCreditos;

    [Header("Botones de Izquierda y Derecha con imagen a desactivar")]
    public List<BotonSimpleConfiguracion> botonesSimples;

    [Header("Pantalla")]
    public Button botonPantallaIzquierda;
    public Button botonPantallaDerecha;
    public GameObject[] textosPantalla;
    private int indexPantalla = 0;

    [Header("Resolución")]
    public Button botonResolucionIzquierda;
    public Button botonResolucionDerecha;
    public GameObject[] textosResolucion;
    private int indexResolucion = 1;

    [Header("Calidad Gráfica")]
    public Button botonGraficaIzquierda;
    public Button botonGraficaDerecha;
    public GameObject[] textosGrafica;
    private int indexGrafica = 2;

    [Header("Sliders de Sonido")]
    public Slider sliderGeneral;
    public Slider sliderMusica;
    public Slider sliderSFX;

    [Header("Idiomas")]
    public Button botonEspañol;
    public Button botonRuso;
    public Button botonIngles;
    public Button botonJapones;
    public GameObject imagenEspañol;
    public GameObject imagenRuso;
    public GameObject imagenIngles;
    public GameObject imagenJapones;

    [Header("Controles")]
    public Button botonTeclado;
    public Button botonMando;
    public GameObject imagenTeclado;
    public GameObject imagenMando;

    [Header("Créditos")]
    public TextMeshProUGUI textoCreditos;
    public float duracionEscrituraCreditos = 0.05f;
    private string textoCreditosCompleto = "Créditos del Juego: Desarrollador: [Nombre]. Música por: [Músico].";


    private void Start()
    {
        ConfigurarBotones(botonesMenu);
        ConfigurarBotones(botonesOpciones);
        ConfigurarBoton(botonVolverVideo);
        ConfigurarBoton(botonVolverSonido);
        ConfigurarBoton(botonVolverIdiomas);
        ConfigurarBoton(botonVolverControles);
        ConfigurarBoton(botonVolverCreditos);
        ConfigurarBotonesSimples(botonesSimples);

        // Configurar botones de idioma
        ConfigurarBotonIdioma(botonEspañol, imagenEspañol);
        ConfigurarBotonIdioma(botonRuso, imagenRuso);
        ConfigurarBotonIdioma(botonIngles, imagenIngles);
        ConfigurarBotonIdioma(botonJapones, imagenJapones);

        botonPantallaIzquierda.onClick.AddListener(() => CambiarPantalla(-1));
        botonPantallaDerecha.onClick.AddListener(() => CambiarPantalla(1));
        botonResolucionIzquierda.onClick.AddListener(() => CambiarResolucion(-1));
        botonResolucionDerecha.onClick.AddListener(() => CambiarResolucion(1));
        botonGraficaIzquierda.onClick.AddListener(() => CambiarGrafica(-1));
        botonGraficaDerecha.onClick.AddListener(() => CambiarGrafica(1));

        ActualizarPantalla();
        ActualizarResolucion();
        ActualizarGrafica();

        sliderGeneral.onValueChanged.AddListener((v) => PlayerPrefs.SetFloat("VolumenGeneral", v));
        sliderMusica.onValueChanged.AddListener((v) => PlayerPrefs.SetFloat("VolumenMusica", v));
        sliderSFX.onValueChanged.AddListener((v) => PlayerPrefs.SetFloat("VolumenSFX", v));

        sliderGeneral.value = PlayerPrefs.GetFloat("VolumenGeneral", 1);
        sliderMusica.value = PlayerPrefs.GetFloat("VolumenMusica", 1);
        sliderSFX.value = PlayerPrefs.GetFloat("VolumenSFX", 1);

        botonTeclado.onClick.AddListener(() =>
        {
            if (imagenTeclado != null) imagenTeclado.SetActive(true);
            if (imagenMando != null) imagenMando.SetActive(false);
        });

        botonMando.onClick.AddListener(() =>
        {
            if (imagenTeclado != null) imagenTeclado.SetActive(false);
            if (imagenMando != null) imagenMando.SetActive(true);
        });

        // Opcional: Efecto de hover para desactivar temporalmente imagen asociada
        ConfigurarHover(botonTeclado, imagenTeclado);
        ConfigurarHover(botonMando, imagenMando);

    }

    void ConfigurarBoton(BotonConfiguracion config)
    {
        if (config.boton == null) return;

        EventTrigger trigger = config.boton.GetComponent<EventTrigger>() ?? config.boton.gameObject.AddComponent<EventTrigger>();

        var enter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        enter.callback.AddListener((eventData) =>
        {
            if (config.imagenADesactivar != null)
                config.imagenADesactivar.SetActive(false);
        });
        trigger.triggers.Add(enter);

        var exit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        exit.callback.AddListener((eventData) =>
        {
            if (config.imagenADesactivar != null)
                config.imagenADesactivar.SetActive(true);
        });
        trigger.triggers.Add(exit);

        config.boton.onClick.AddListener(() =>
        {
            EjecutarAccion(config);

            if (config.nombre.ToLower().Contains("creditos"))
            {
                StartCoroutine(AnimarTextoCreditos());
            }
        });
    }

    void ConfigurarBotones(List<BotonConfiguracion> botones)
    {
        foreach (var config in botones)
            ConfigurarBoton(config);
    }

    void ConfigurarBotonesSimples(List<BotonSimpleConfiguracion> botones)
    {
        foreach (var simple in botones)
        {
            if (simple.boton == null) continue;

            EventTrigger trigger = simple.boton.GetComponent<EventTrigger>() ?? simple.boton.gameObject.AddComponent<EventTrigger>();

            var enter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            enter.callback.AddListener((eventData) =>
            {
                if (simple.imagenADesactivar != null)
                    simple.imagenADesactivar.SetActive(false);
            });
            trigger.triggers.Add(enter);

            var exit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
            exit.callback.AddListener((eventData) =>
            {
                if (simple.imagenADesactivar != null)
                    simple.imagenADesactivar.SetActive(true);
            });
            trigger.triggers.Add(exit);
        }
    }

    void ConfigurarBotonIdioma(Button boton, GameObject imagen)
    {
        EventTrigger trigger = boton.GetComponent<EventTrigger>() ?? boton.gameObject.AddComponent<EventTrigger>();

        var enter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        enter.callback.AddListener((eventData) => { if (imagen != null) imagen.SetActive(false); });
        trigger.triggers.Add(enter);

        var exit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        exit.callback.AddListener((eventData) => { if (imagen != null) imagen.SetActive(true); });
        trigger.triggers.Add(exit);

        boton.onClick.AddListener(() => CambiarIdioma(imagen));
    }

    void ConfigurarHover(Button boton, GameObject imagen)
    {
        if (boton == null || imagen == null) return;

        EventTrigger trigger = boton.GetComponent<EventTrigger>() ?? boton.gameObject.AddComponent<EventTrigger>();

        var enter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        enter.callback.AddListener((eventData) => imagen.SetActive(false));
        trigger.triggers.Add(enter);

        var exit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        exit.callback.AddListener((eventData) => imagen.SetActive(true));
        trigger.triggers.Add(exit);
    }


    void CambiarIdioma(GameObject imagen)
    {
        if (imagen == null) return;

        imagenEspañol.SetActive(imagenEspañol == imagen);
        imagenRuso.SetActive(imagenRuso == imagen);
        imagenIngles.SetActive(imagenIngles == imagen);
        imagenJapones.SetActive(imagenJapones == imagen);
    }

    void EjecutarAccion(BotonConfiguracion config)
    {
        switch (config.accion)
        {
            case TipoAccion.IrAEscena:
                if (!string.IsNullOrEmpty(config.datoAccion))
                    SceneManager.LoadScene(config.datoAccion);
                break;
            case TipoAccion.AbrirURL:
                if (!string.IsNullOrEmpty(config.datoAccion))
                    Application.OpenURL(config.datoAccion);
                break;
            case TipoAccion.MoverCamara:
                if (config.destinoCamara != null)
                    StartCoroutine(MoverCamara(config.destinoCamara.position));
                break;
            case TipoAccion.Salir:
                Application.Quit();
                break;
        }
    }

    IEnumerator MoverCamara(Vector3 destino)
    {
        Vector3 origen = camara.position;
        float duracion = velocidadMovimiento;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;
            t = Mathf.SmoothStep(0f, 1f, t); // aceleración–desaceleración suave
            camara.position = Vector3.Lerp(origen, destino, t);
            yield return null;
        }

        camara.position = destino;
    }

    IEnumerator AnimarTextoCreditos()
    {
        textoCreditos.text = "";
        for (int i = 0; i <= textoCreditosCompleto.Length; i++)
        {
            textoCreditos.text = textoCreditosCompleto.Substring(0, i);
            yield return new WaitForSeconds(duracionEscrituraCreditos);
        }
    }

    void CambiarPantalla(int direccion)
    {
        indexPantalla = (indexPantalla + direccion + textosPantalla.Length) % textosPantalla.Length;
        ActualizarPantalla();
    }

    void ActualizarPantalla()
    {
        for (int i = 0; i < textosPantalla.Length; i++)
            textosPantalla[i].SetActive(i == indexPantalla);

        switch (indexPantalla)
        {
            case 0: Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; break;
            case 1: Screen.fullScreenMode = FullScreenMode.Windowed; break;
            case 2: Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
        }
    }

    void CambiarResolucion(int direccion)
    {
        indexResolucion = (indexResolucion + direccion + textosResolucion.Length) % textosResolucion.Length;
        ActualizarResolucion();
    }

    void ActualizarResolucion()
    {
        for (int i = 0; i < textosResolucion.Length; i++)
            textosResolucion[i].SetActive(i == indexResolucion);

        switch (indexResolucion)
        {
            case 0: Screen.SetResolution(1280, 720, Screen.fullScreenMode); break;
            case 1: Screen.SetResolution(1920, 1080, Screen.fullScreenMode); break;
            case 2: Screen.SetResolution(2560, 1440, Screen.fullScreenMode); break;
            case 3: Screen.SetResolution(3840, 2160, Screen.fullScreenMode); break;
        }
    }

    void CambiarGrafica(int direccion)
    {
        indexGrafica = (indexGrafica + direccion + textosGrafica.Length) % textosGrafica.Length;
        ActualizarGrafica();
    }

    void ActualizarGrafica()
    {
        for (int i = 0; i < textosGrafica.Length; i++)
            textosGrafica[i].SetActive(i == indexGrafica);

        QualitySettings.SetQualityLevel(indexGrafica);
    }
}
