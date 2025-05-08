using UnityEngine;

public class RotacionLimitadaCamara : MonoBehaviour
{
    [Header("Velocidad de rotación")]
    public float sensibilidadX = 5f;
    public float sensibilidadY = 5f;

    [Header("Límites de rotación (en grados desde la rotación inicial)")]
    public float limiteX = 30f; // Límite de inclinación vertical (arriba/abajo)
    public float limiteY = 60f; // Límite de giro horizontal (izquierda/derecha)

    [Header("Suavizado (opcional)")]
    public float suavizado = 5f;

    private Vector2 rotacionActual; // en grados
    private Vector2 rotacionObjetivo;

    private Quaternion rotacionInicial;

    void Start()
    {
        rotacionInicial = transform.localRotation;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");

        // Ajustamos la rotación con la entrada del ratón, pero respetamos la rotación de la cámara en el eje Z
        rotacionObjetivo.x += inputX * sensibilidadX;
        rotacionObjetivo.y -= inputY * sensibilidadY;

        // Limitar la rotación en los ejes X (vertical) y Y (horizontal)
        rotacionObjetivo.x = Mathf.Clamp(rotacionObjetivo.x, -limiteY, limiteY);
        rotacionObjetivo.y = Mathf.Clamp(rotacionObjetivo.y, -limiteX, limiteX);

        // Rotación final de la cámara
        Quaternion rotacionFinal = rotacionInicial * Quaternion.Euler(rotacionObjetivo.y, rotacionObjetivo.x, 0);

        // Suavizado para que el movimiento de la cámara sea más fluido
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotacionFinal, Time.deltaTime * suavizado);
    }
}
