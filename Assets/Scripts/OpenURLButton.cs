using UnityEngine;

public class OpenURLButton : MonoBehaviour
{
    [Header("URL a abrir")]
    [Tooltip("Introduce aquí la URL que quieres abrir.")]
    public string url = "https://dmichay.itch.io/nemunai";

    // Esta función se puede vincular al evento OnClick del botón
    public void AbrirPaginaWeb()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogWarning("La URL está vacía o no se ha asignado.");
        }
    }
}
