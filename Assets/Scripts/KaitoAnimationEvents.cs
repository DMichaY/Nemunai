using UnityEngine;

public class KaitoAnimationEvents : MonoBehaviour
{
    private KaitoSurvival kaitoSurvival;

    void Start()
    {
        kaitoSurvival = GetComponentInParent<KaitoSurvival>(); // Encuentra el script en el padre
    }

    // 🔹 Asegúrate de que el método sea público y sin parámetros
    public void EndInteraction()
    {
        if (kaitoSurvival != null)
        {
            kaitoSurvival.EndInteraction(); // Llama al método en el padre
        }
    }
}
