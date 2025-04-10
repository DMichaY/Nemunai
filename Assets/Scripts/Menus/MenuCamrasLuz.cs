using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuCamrasLuz : MonoBehaviour
{
    [Header("Spotlight")]
    public Transform foco; // El spotlight a mover
    public float velocidadRotacion = 10f;

    private Transform objetivoActual;

    [Header("Cámaras")]
    public Camera camaraPrincipal;
    public Camera camaraOpciones;
    public Camera camaraIdiomas;

    private Camera camaraActivaAnterior;

    [Header("Botones")]
    public Button botonJugar;
    public Button botonOpciones;
    public Button botonSalir;
    public Button botonItchio;
    public Button botonVolver;

    void Start()
    {
        // Activar solo la cámara principal al inicio
        ActivarSoloEstaCamara(camaraPrincipal);

        // Asignar eventos de ratón a cada botón del menú inicial
        AsignarEventos(botonJugar);
        AsignarEventos(botonOpciones);
        AsignarEventos(botonSalir);
        AsignarEventos(botonItchio);

        // Asignar funciones a botones
        botonOpciones.onClick.AddListener(() => CambiarCamara(camaraOpciones));
        botonVolver.onClick.AddListener(() => VolverACamaraAnterior());
    }

    void Update()
    {
        if (foco != null && objetivoActual != null)
        {
            Vector3 direccion = objetivoActual.position - foco.position;
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
            foco.rotation = Quaternion.Lerp(foco.rotation, rotacionDeseada, Time.deltaTime * velocidadRotacion);
        }
    }

    void AsignarEventos(Button boton)
    {
        EventTrigger trigger = boton.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = boton.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entrada = new EventTrigger.Entry();
        entrada.eventID = EventTriggerType.PointerEnter;
        entrada.callback.AddListener((data) => { objetivoActual = boton.transform; });
        trigger.triggers.Add(entrada);
    }

    void CambiarCamara(Camera nuevaCamara)
    {
        camaraActivaAnterior = Camera.main;
        ActivarSoloEstaCamara(nuevaCamara);
    }

    void VolverACamaraAnterior()
    {
        if (camaraActivaAnterior != null)
            ActivarSoloEstaCamara(camaraActivaAnterior);
    }

    void ActivarSoloEstaCamara(Camera camaraAActivar)
    {
        // Desactivar todas
        camaraPrincipal.gameObject.SetActive(false);
        camaraOpciones.gameObject.SetActive(false);
        camaraIdiomas.gameObject.SetActive(false);

        // Activar solo la elegida
        camaraAActivar.gameObject.SetActive(true);
    }
}