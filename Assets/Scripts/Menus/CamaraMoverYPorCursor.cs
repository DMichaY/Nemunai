using UnityEngine;

public class CamaraMoverYPorCursor : MonoBehaviour
{
    [Header("Configuraci�n del movimiento")]
    public float velocidad = 5f;
    public float limiteInferiorY = -5f;
    public float limiteSuperiorY = 5f;

    [Header("Zona de activaci�n en p�xeles")]
    public float margenSuperior = 50f;
    public float margenInferior = 50f;

    [Header("Apartado del men�")]
    public GameObject menuPanel; // <-- A�ADIDO: referencia al apartado del men�

    private bool estabaActivoAntes = true; // <-- A�ADIDO: para detectar cambios de estado

    void Update()
    {
        // A�ADIDO: verificar si el estado del men� ha cambiado
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

        // Si el cursor est� cerca del borde superior
        if (posicionCursorY >= alturaPantalla - margenSuperior)
        {
            posicionCamara.y += velocidad * Time.deltaTime;
        }
        // Si el cursor est� cerca del borde inferior
        else if (posicionCursorY <= margenInferior)
        {
            posicionCamara.y -= velocidad * Time.deltaTime;
        }

        // Limitar movimiento en Y
        posicionCamara.y = Mathf.Clamp(posicionCamara.y, limiteInferiorY, limiteSuperiorY);

        transform.position = posicionCamara;
    }
}
