using UnityEngine;

public class ControladorActivacionCamara : MonoBehaviour
{
    public GameObject menuPanel; // Asigna aquí el apartado "Menú"
    public CamaraMoverYPorCursor camaraMover; // Asigna aquí el script que quieres activar/desactivar

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
