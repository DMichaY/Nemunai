using UnityEngine;

public class Teletransporte : MonoBehaviour
{
    public Transform destino;
    public string tagJugador = "Player";

    private void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag(tagJugador))
        {
            if (destino != null)
            {
                otro.transform.position = destino.position;
                otro.transform.rotation = destino.rotation;
            }
        }
    }
}
