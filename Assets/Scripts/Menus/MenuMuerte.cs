using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
public class MenuMuerte : MonoBehaviour
{
        public bool pausado = false;

    [Header("Paneles principales")]
    public GameObject panelPausa;
    public GameObject apartadoPausa;
    public GameObject apartadoConfirmarVolver;
    public GameObject apartadoConfirmarSalir;
    

    [Header("Botones y sus imágenes a ocultar")]
    public Button btnContinuar, btnVolverMenu, btnSalir;
    public Button btnMenuSi, btnMenuNo, btnSalirSi, btnSalirNo;
    public GameObject imgBtnContinuar, imgBtnVolverMenu, imgBtnSalir;
    public GameObject imgBtnMenuSi, imgBtnMenuNo, imgBtnSalirSi, imgBtnSalirNo;



    [Header("Clips de audio")]
    public AudioClip sonidoHover;
    public AudioClip sonidoClick;

    [Header("Mixer de audio y grupos")]
    public AudioMixer mezcladorPrincipal;
    public AudioMixerGroup grupoMusica;
    public AudioMixerGroup grupoSFX;

    private AudioSource fuenteSFX;

    public string nombreEscena;



    void Awake()
    {
        // Crear AudioSource efectos
        GameObject objetoSFX = new GameObject("FuenteAudioSFX");
        objetoSFX.transform.parent = this.transform;
        fuenteSFX = objetoSFX.AddComponent<AudioSource>();
        fuenteSFX.outputAudioMixerGroup = grupoSFX;

        // Asignar eventos de audio a todos los botones en la escena
        foreach (Button boton in FindObjectsOfType<Button>())
        {
            AsignarEventosAudio(boton);
        }
    }

    void Start()
    {
        panelPausa.SetActive(false);
        

        btnContinuar.onClick.AddListener(Continuar);
        btnVolverMenu.onClick.AddListener(() => AlternarApartado(apartadoPausa, apartadoConfirmarVolver));
        btnSalir.onClick.AddListener(() => AlternarApartado(apartadoPausa, apartadoConfirmarSalir));

        btnMenuSi.onClick.AddListener(VolverAlMenu);
        btnMenuNo.onClick.AddListener(() => AlternarApartado(apartadoConfirmarVolver, apartadoPausa));
        btnSalirSi.onClick.AddListener(SalirDelJuego);
        btnSalirNo.onClick.AddListener(() => AlternarApartado(apartadoConfirmarSalir, apartadoPausa));
        
    
        AsignarEventosHover();

       
    }



    void Continuar()
    {

        SceneManager.LoadScene(nombreEscena);

    }


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
        apartadoConfirmarVolver.SetActive(false);
        apartadoConfirmarSalir.SetActive(false);
        
        apartado.SetActive(true);
    }

    void AsignarEventosHover()
    {
        AsignarHover(btnContinuar, imgBtnContinuar);
        AsignarHover(btnVolverMenu, imgBtnVolverMenu);
        AsignarHover(btnSalir, imgBtnSalir);
        AsignarHover(btnMenuSi, imgBtnMenuSi);
        AsignarHover(btnMenuNo, imgBtnMenuNo);
        AsignarHover(btnSalirSi, imgBtnSalirSi);
        AsignarHover(btnSalirNo, imgBtnSalirNo);
       
        
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

   

    void AsignarEventosAudio(Button boton)
    {
        EventTrigger disparador = boton.gameObject.GetComponent<EventTrigger>();
        if (disparador == null)
            disparador = boton.gameObject.AddComponent<EventTrigger>();

        // Evento al pasar el cursor (hover)
        EventTrigger.Entry entradaHover = new EventTrigger.Entry();
        entradaHover.eventID = EventTriggerType.PointerEnter;
        entradaHover.callback.AddListener((eventData) => ReproducirSFX(sonidoHover));
        disparador.triggers.Add(entradaHover);

        // Evento al hacer clic
        boton.onClick.AddListener(() => ReproducirSFX(sonidoClick));
    }

    void ReproducirSFX(AudioClip clip)
    {
        if (clip != null && fuenteSFX != null)
        {
            fuenteSFX.PlayOneShot(clip);
        }
    }
}
