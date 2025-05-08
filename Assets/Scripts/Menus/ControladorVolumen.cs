using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControladorVolumen : MonoBehaviour
{
    [Header("Referencia al AudioMixer")]
    public AudioMixer mezclador;

    [Header("Nombre del parámetro expuesto en el mixer (ej. Volume_Music, Volume_SFX)")]
    public string nombreParametro;

    [Header("Slider de UI asignado automáticamente")]
    public Slider slider;

    private void Start()
    {
        // Asegúrate de que el slider esté vinculado
        if (slider == null)
            slider = GetComponent<Slider>();

        // Carga valor guardado (opcional)
        if (PlayerPrefs.HasKey(nombreParametro))
        {
            float valorGuardado = PlayerPrefs.GetFloat(nombreParametro);
            slider.value = valorGuardado;
            CambiarVolumen(valorGuardado);
        }

        // Asigna evento
        slider.onValueChanged.AddListener(CambiarVolumen);
    }

    public void CambiarVolumen(float valor)
    {
        // Convierte valor de 0-1 a decibelios (-80 a 0)
        float dB = Mathf.Log10(Mathf.Clamp(valor, 0.0001f, 1f)) * 20;
        mezclador.SetFloat(nombreParametro, dB);

        // Guarda el valor (opcional)
        PlayerPrefs.SetFloat(nombreParametro, valor);
    }
}
