using UnityEngine;

public class Teletransportador : MonoBehaviour
{
    [Header("Referencias")]
    public Transform puntoSalida; // Asigna el GameObject vacío de destino en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && puntoSalida != null)
        {
            other.transform.position = puntoSalida.position;
        }
    }
}
