using UnityEngine;
using UnityEngine.EventSystems;

public class RotadorArrow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float velocidadRotacion = 180f;
    private bool rotar = false;
    public Transform arrow;

    void Start()
    {

    }

    void Update()
    {
        if (rotar && arrow != null)
        {
            arrow.Rotate(Vector3.forward * velocidadRotacion * Time.deltaTime);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rotar = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rotar = false;
    }
}
