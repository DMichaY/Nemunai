using UnityEngine;

public class CamaraSeguirJugador : MonoBehaviour
{
    public Transform objetivo; // El jugador a seguir

    void Update()
    {
        if (objetivo != null)
        {
            transform.LookAt(objetivo);
        }
    }
}
