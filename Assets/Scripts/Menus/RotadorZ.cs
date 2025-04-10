using UnityEngine;

public class RotadorZ : MonoBehaviour
{
    [SerializeField] private float velocidadRotacion = 90f; // Grados por segundo

    void Update()
    {
        transform.Rotate(0f, 0f, velocidadRotacion * Time.deltaTime);
    }
}
