using UnityEngine;

public class ActivarAlDesactivar : MonoBehaviour
{
    [Header("Objeto que se activará al entrar en el trigger")]
    public GameObject objetoActivar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && objetoActivar != null)
        {
            objetoActivar.SetActive(true);
        }
    }
}
