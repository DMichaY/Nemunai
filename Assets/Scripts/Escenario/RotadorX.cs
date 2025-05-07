using UnityEngine;

public class RotadorX : MonoBehaviour
{
    [SerializeField] private float velocidadRotacion = 90f; // Grados por segundo

    void Update()
    {
        transform.Rotate(velocidadRotacion * Time.deltaTime, 0f, 0f);
    }
}
