using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Configuración del parpadeo")]
    public float tiempoApagada = 1.5f;
    public float duracionEncendido = 0.1f;
    public float duracionApagado = 0.5f;
    public float intensidadMaxima = 1.5f;

    [Header("Configuración del Emission")]
    public Renderer objetoConMaterial;
    public int indiceMaterial = 0;
    public Color colorEmission = Color.white;
    public float intensidadEmissionMaxima = 1.5f;

    private Material materialEmission;
    private Light luz;

    private enum Estado { Apagada, Encendiendo, Apagando }
    private Estado estadoActual = Estado.Apagada;
    private float tiempoEstado = 0f;

    void Start()
    {
        luz = GetComponent<Light>();
        luz.intensity = 0f;

        if (objetoConMaterial != null && indiceMaterial < objetoConMaterial.materials.Length)
        {
            // Importante: usamos materials (instancia) y no sharedMaterials (global)
            materialEmission = objetoConMaterial.materials[indiceMaterial];
            materialEmission.EnableKeyword("_EMISSION");
        }
        else
        {
            Debug.LogWarning("Material no asignado correctamente o índice fuera de rango.");
        }
    }

    void Update()
    {
        tiempoEstado += Time.deltaTime;

        float intensidadActual = 0f;

        switch (estadoActual)
        {
            case Estado.Apagada:
                intensidadActual = 0f;
                if (tiempoEstado >= tiempoApagada)
                    CambiarEstado(Estado.Encendiendo);
                break;

            case Estado.Encendiendo:
                float tEnc = tiempoEstado / duracionEncendido;
                intensidadActual = Mathf.Lerp(0f, intensidadMaxima, tEnc);
                if (tiempoEstado >= duracionEncendido)
                    CambiarEstado(Estado.Apagando);
                break;

            case Estado.Apagando:
                float tApag = tiempoEstado / duracionApagado;
                intensidadActual = Mathf.Lerp(intensidadMaxima, 0f, tApag);
                if (tiempoEstado >= duracionApagado)
                    CambiarEstado(Estado.Apagada);
                break;
        }

        // Actualizar la intensidad de la luz
        luz.intensity = intensidadActual;

        // Actualizar Emission del material (si se asignó)
        if (materialEmission != null)
        {
            float intensidadEmission = intensidadActual / intensidadMaxima * intensidadEmissionMaxima;
            materialEmission.SetColor("_EmissionColor", colorEmission * intensidadEmission);
        }
    }

    void CambiarEstado(Estado nuevoEstado)
    {
        estadoActual = nuevoEstado;
        tiempoEstado = 0f;
    }
}