using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMPManager : MonoBehaviour
{
    [Tooltip("Lista de TMPs gestionados. Solo uno podrá estar activo a la vez.")]
    public List<TextMeshProUGUI> tmpList = new List<TextMeshProUGUI>();

    private TextMeshProUGUI currentActiveTMP = null;

    void Update()
    {
        // Revisión continua para detectar activaciones externas
        foreach (var tmp in tmpList)
        {
            if (tmp.gameObject.activeSelf && tmp != currentActiveTMP)
            {
                ForceActivate(tmp);
                break; // Solo permitimos uno activo, salimos del bucle
            }
        }
    }

    public void ActivateTMP(TextMeshProUGUI tmpToActivate)
    {
        if (!tmpList.Contains(tmpToActivate))
        {
            Debug.LogWarning("Este TMP no está en la lista del TMPManager.");
            return;
        }

        ForceActivate(tmpToActivate);
    }

    private void ForceActivate(TextMeshProUGUI tmpToActivate)
    {
        // Desactiva el anterior si es diferente
        if (currentActiveTMP != null && currentActiveTMP != tmpToActivate)
        {
            currentActiveTMP.gameObject.SetActive(false);
        }

        // Activa el nuevo
        tmpToActivate.gameObject.SetActive(true);
        currentActiveTMP = tmpToActivate;
    }

    public void DeactivateCurrent()
    {
        if (currentActiveTMP != null)
        {
            currentActiveTMP.gameObject.SetActive(false);
            currentActiveTMP = null;
        }
    }
}
