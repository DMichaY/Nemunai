using UnityEngine;

public class FlotadorAlma : MonoBehaviour
{
    public float amplitud = 1f;         // Qué tanto sube y baja (de su posición original)
    public float velocidadMin = 0.5f;   // Velocidad mínima del movimiento
    public float velocidadMax = 1.5f;   // Velocidad máxima del movimiento

    private Vector3 posicionInicial;
    private float velocidadActual;
    private float offsetFase;

    void Start()
    {
        posicionInicial = transform.position;
        velocidadActual = Random.Range(velocidadMin, velocidadMax);
        offsetFase = Random.Range(0f, 2f * Mathf.PI); // Para que no todas empiecen igual
    }

    void Update()
    {
        float nuevaY = posicionInicial.y + Mathf.Sin(Time.time * velocidadActual + offsetFase) * amplitud;
        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
    }
}
