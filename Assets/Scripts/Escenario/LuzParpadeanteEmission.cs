using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LuzParpadeanteEmission : MonoBehaviour
{
    [Header("Referencias necesarias")]
    [Tooltip("Luz que representa la bombilla/farola")]
    public Light farolaLight;

    [Tooltip("Material compartido que tiene emisión")]
    public Material emissionMaterialBase;

    [Header("Control de Flicker")]
    [Tooltip("Intervalo mínimo entre parpadeos")]
    public float minFlickerInterval = 0.05f;

    [Tooltip("Intervalo máximo entre parpadeos")]
    public float maxFlickerInterval = 0.3f;

    [Tooltip("Intensidad máxima de la luz y emisión")]
    public float maxIntensity = 2f;

    [Tooltip("Color de emisión")]
    public Color emissionColor = Color.white;

    private Material instanceMaterial; // material instanciado por objeto
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();

        // Buscamos el índice del material base en el array de materiales
        int index = System.Array.IndexOf(rend.sharedMaterials, emissionMaterialBase);
        if (index == -1)
        {
            Debug.LogWarning($"El material de emisión no se encuentra en {gameObject.name}", this);
            return;
        }

        // Instanciamos el material solo para esta rendición
        Material[] materials = rend.materials;
        instanceMaterial = new Material(emissionMaterialBase); // instancia nueva
        materials[index] = instanceMaterial;
        rend.materials = materials;
    }

    private void Start()
    {
        if (instanceMaterial != null && farolaLight != null)
        {
            StartCoroutine(FlickerRoutine());
        }
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            // Random entre 0 y 1 para decidir si "falla"
            bool isFlickering = Random.value > 0.5f;

            float targetIntensity = isFlickering ? Random.Range(0f, maxIntensity) : maxIntensity;

            // Aplicar a la luz
            farolaLight.intensity = targetIntensity;

            // Aplicar a la emisión del material (con _EMISSION activado)
            Color finalEmission = emissionColor * targetIntensity;
            instanceMaterial.SetColor("_EmissionColor", finalEmission);
            DynamicGI.SetEmissive(rend, finalEmission); // Para iluminación global si se usa

            // Espera aleatoria entre parpadeos
            float waitTime = Random.Range(minFlickerInterval, maxFlickerInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}