using UnityEngine;

public class ControladorActivacionCamara : MonoBehaviour
{
    public GameObject menuPanel; // Asigna aqu� el apartado "Men�"
    public CamaraMoverYPorCursor camaraMover; // Asigna aqu� el script que quieres activar/desactivar

    private bool estadoAnterior = true;

    void Update()
    {
        if (menuPanel == null || camaraMover == null) return;

        bool menuActivo = menuPanel.activeSelf;

        if (menuActivo != estadoAnterior)
        {
            camaraMover.enabled = menuActivo;
            estadoAnterior = menuActivo;
        }
    }
}
