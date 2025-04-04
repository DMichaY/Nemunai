using UnityEngine;

public class RotacionAleatoriaZ : MonoBehaviour
{
    void Start()
    {
        RotarAleatoriamente();
    }

    void RotarAleatoriamente()
    {
        float anguloAleatorio = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, anguloAleatorio);
    }
}
