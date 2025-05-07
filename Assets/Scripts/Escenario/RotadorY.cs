using UnityEngine;

public class RotadorY : MonoBehaviour
{
    [SerializeField] private float velocidadRotacion = 90f; // Grados por segundo

    void Update()
    {
        transform.Rotate(0f, velocidadRotacion * Time.deltaTime, 0f);
    }
}
