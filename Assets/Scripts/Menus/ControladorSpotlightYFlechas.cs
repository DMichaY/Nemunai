using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControladorSpotlightYFlechas : MonoBehaviour
{
    [Header("Spotlight")]
    public Transform spotlight;
    public float distanciaZ = 10f;

    [Header("Rotación de Arrow")]
    public float velocidadRotacion = 180f;

    void Update()
    {
        MoverSpotlightConMouse();
    }

    void MoverSpotlightConMouse()
    {
        Vector3 posicionRaton = Input.mousePosition;
        posicionRaton.z = distanciaZ;
        Vector3 posicionMundo = Camera.main.ScreenToWorldPoint(posicionRaton);

        spotlight.position = new Vector3(posicionMundo.x, posicionMundo.y, spotlight.position.z);
    }
}
