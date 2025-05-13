using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlImagenAnimada : MonoBehaviour
{
    public RectTransform imagenCanvas; // La imagen del canvas que se moverá
    public float duracionAnimacion = 0.5f;
    public Vector2 posicionFuera = new Vector2(0, -800);
    public Vector2 posicionCentro = new Vector2(0, 0);

    private bool imagenActiva = false;
    private bool animando = false;

    void Start()
    {
        imagenCanvas.anchoredPosition = posicionFuera;
        imagenCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (imagenActiva && !animando && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(OcultarImagen());
        }
    }

    public void MostrarImagen()
    {
        StartCoroutine(MostrarImagenAnimada());
    }

    IEnumerator MostrarImagenAnimada()
    {
        imagenCanvas.gameObject.SetActive(true);
        animando = true;
        GestorEntrada.BloquearEntrada();

        float tiempo = 0;
        while (tiempo < duracionAnimacion)
        {
            imagenCanvas.anchoredPosition = Vector2.Lerp(posicionFuera, posicionCentro, tiempo / duracionAnimacion);
            tiempo += Time.unscaledDeltaTime;
            yield return null;
        }

        imagenCanvas.anchoredPosition = posicionCentro;
        animando = false;
        imagenActiva = true;
    }

    IEnumerator OcultarImagen()
    {
        animando = true;

        float tiempo = 0;
        while (tiempo < duracionAnimacion)
        {
            imagenCanvas.anchoredPosition = Vector2.Lerp(posicionCentro, posicionFuera, tiempo / duracionAnimacion);
            tiempo += Time.unscaledDeltaTime;
            yield return null;
        }

        imagenCanvas.anchoredPosition = posicionFuera;
        imagenCanvas.gameObject.SetActive(false);
        imagenActiva = false;
        animando = false;

        GestorEntrada.DesbloquearEntrada();
    }
}
