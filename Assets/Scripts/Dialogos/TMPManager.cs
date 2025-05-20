using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPManager : MonoBehaviour
{
    public List<TextMeshProUGUI> tmpList = new List<TextMeshProUGUI>();

    private TextMeshProUGUI currentActiveTMP = null;

    // Llama a esto desde fuera cuando actives un TMP
    public void ActivateTMP(TextMeshProUGUI tmpToActivate)
    {
        if (!tmpList.Contains(tmpToActivate))
        {
            Debug.LogWarning("Este TMP no está en la lista del TMPManager.");
            return;
        }

        // Si hay uno activo diferente, lo desactivamos
        if (currentActiveTMP != null && currentActiveTMP != tmpToActivate)
        {
            currentActiveTMP.gameObject.SetActive(false);
        }

        // Activamos el nuevo
        tmpToActivate.gameObject.SetActive(true);
        currentActiveTMP = tmpToActivate;
    }

    // Opcional: para desactivar el actual desde fuera
    public void DeactivateCurrent()
    {
        if (currentActiveTMP != null)
        {
            currentActiveTMP.gameObject.SetActive(false);
            currentActiveTMP = null;
        }
    }
}
