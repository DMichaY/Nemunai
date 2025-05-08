using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AdministradorAudioBotones : MonoBehaviour
{
    [Header("Clips de audio")]
    public AudioClip sonidoHover;
    public AudioClip sonidoClick;
    public AudioClip musicaFondo;

    [Header("Mixer de audio y grupos")]
    public AudioMixer mezcladorPrincipal;
    public AudioMixerGroup grupoMusica;
    public AudioMixerGroup grupoSFX;

    private AudioSource fuenteMusica;
    private AudioSource fuenteSFX;

    void Awake()
    {
        // Crear dos AudioSource: uno para la música y otro para efectos
        GameObject objetoMusica = new GameObject("FuenteAudioMusica");
        objetoMusica.transform.parent = this.transform;
        fuenteMusica = objetoMusica.AddComponent<AudioSource>();
        fuenteMusica.outputAudioMixerGroup = grupoMusica;
        fuenteMusica.loop = true;

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
        // Reproducir música de fondo al iniciar la escena
        if (musicaFondo != null)
        {
            fuenteMusica.clip = musicaFondo;
            fuenteMusica.Play();
        }
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
