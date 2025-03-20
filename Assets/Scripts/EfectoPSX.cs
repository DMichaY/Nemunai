using UnityEngine;

[ExecuteInEditMode]
public class EfectoPSX : MonoBehaviour
{
    // Resolución "baja" para el efecto pixelado
    public int tamanoPixelX = 320;  // Resolución en el eje X (ancho)
    public int tamanoPixelY = 240;  // Resolución en el eje Y (alto)

    void Start()
    {
        // Obtener todas las cámaras en la escena
        Camera[] todasLasCamaras = Camera.allCameras;

        foreach (Camera camara in todasLasCamaras)
        {
            if (camara != null)
            {
                // Crear un RenderTexture con la resolución deseada
                RenderTexture texturaRenderizada = new RenderTexture(tamanoPixelX, tamanoPixelY, 24);
                camara.targetTexture = texturaRenderizada;  // Asignar el RenderTexture a cada cámara

                // Cambiar el aspecto de la cámara para que se vea más pixelado
                camara.aspect = (float)tamanoPixelX / tamanoPixelY;
            }
        }
    }

    // Este método se llama cada vez que la imagen se renderiza
    void OnRenderImage(RenderTexture fuente, RenderTexture destino)
    {
        // No necesitamos hacer nada especial aquí si usamos el RenderTexture
        Graphics.Blit(fuente, destino);  // Copiar la imagen de la fuente al destino
    }
}
