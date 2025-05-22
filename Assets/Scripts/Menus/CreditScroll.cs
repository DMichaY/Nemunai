using UnityEngine;
using TMPro;

public class CreditScroll : MonoBehaviour
{
    public RectTransform textRect; // Asigna aquí el TextMeshPro RectTransform
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float speed = 20f;

    void Start()
    {
        textRect.localPosition = startPoint;
    }

    void Update()
    {
        if (textRect.localPosition.y < endPoint.y)
        {
            textRect.localPosition += Vector3.up * speed * Time.deltaTime;
        }
    }
}