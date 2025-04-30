using UnityEngine;

public class CamaraMoverYPorCursor : MonoBehaviour
{
    [Header("Configuración del movimiento")]
    public float velocidad = 5f;
    public float limiteInferiorY = -5f;
    public float limiteSuperiorY = 5f;

    [Header("Zona de activación en píxeles")]
    public float margenSuperior = 50f;
    public float margenInferior = 50f;

    [Header("Apartado del menú")]
    public GameObject menuPanel; // <-- AÑADIDO: referencia al apartado del menú

    private bool estabaActivoAntes = true; // <-- AÑADIDO: para detectar cambios de estado

    void Update()
    {
        // AÑADIDO: verificar si el estado del menú ha cambiado
        if (menuPanel != null)
        {
            if (menuPanel.activeSelf && !estabaActivoAntes)
            {
                enabled = true;
                estabaActivoAntes = true;
            }
            else if (!menuPanel.activeSelf && estabaActivoAntes)
            {
                enabled = false;
                estabaActivoAntes = false;
            }
        }

        MoverSegunCursor();
    }

    void MoverSegunCursor()
    {
        Vector3 posicionCamara = transform.position;
        float posicionCursorY = Input.mousePosition.y;
        float alturaPantalla = Screen.height;

        // Si el cursor está cerca del borde superior
        if (posicionCursorY >= alturaPantalla - margenSuperior)
        {
            posicionCamara.y += velocidad * Time.deltaTime;
        }
        // Si el cursor está cerca del borde inferior
        else if (posicionCursorY <= margenInferior)
        {
            posicionCamara.y -= velocidad * Time.deltaTime;
        }

        // Limitar movimiento en Y
        posicionCamara.y = Mathf.Clamp(posicionCamara.y, limiteInferiorY, limiteSuperiorY);

        transform.position = posicionCamara;
    }
}
