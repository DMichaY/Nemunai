using UnityEngine;

[ExecuteInEditMode]
public class EfectoPSX : MonoBehaviour
{
    // Resoluci�n "baja" para el efecto pixelado
    public int tamanoPixelX = 320;  // Resoluci�n en el eje X (ancho)
    public int tamanoPixelY = 240;  // Resoluci�n en el eje Y (alto)

    void Start()
    {
        // Obtener todas las c�maras en la escena
        Camera[] todasLasCamaras = Camera.allCameras;

        foreach (Camera camara in todasLasCamaras)
        {
            if (camara != null)
            {
                // Crear un RenderTexture con la resoluci�n deseada
                RenderTexture texturaRenderizada = new RenderTexture(tamanoPixelX, tamanoPixelY, 24);
                camara.targetTexture = texturaRenderizada;  // Asignar el RenderTexture a cada c�mara

                // Cambiar el aspecto de la c�mara para que se vea m�s pixelado
                camara.aspect = (float)tamanoPixelX / tamanoPixelY;
            }
        }
    }

    // Este m�todo se llama cada vez que la imagen se renderiza
    void OnRenderImage(RenderTexture fuente, RenderTexture destino)
    {
        // No necesitamos hacer nada especial aqu� si usamos el RenderTexture
        Graphics.Blit(fuente, destino);  // Copiar la imagen de la fuente al destino
    }
}
