using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Añadido para usar TextMeshPro

public class LogicaVerjaEstacion : Interactable
{
    // Variables
    Animator animacionPuertaVerja;
    public GameObject llaveVerja;
    public GameObject verja;
    public GameObject muroVerja;
    public GameObject brillitoLlave;

    public bool tieneLlave;
    public bool llaveUsada;
    public bool antiSpam;

    public GameObject imagenLlave;

    // Texto
    GameObject recordarLlave;
    public TextMeshProUGUI recordarLlaveTMP; // Añadido
    private TypewriterEffect typewriter;     // Añadido

    // Bandas negras
    public RectTransform bandaArriba;
    public RectTransform bandaAbajo;
    public float yPosBandaArriba = 200f;  // Posición Y deseada para la banda superior
    public float yPosBandaAbajo = -200f;  // Posición Y deseada para la banda inferior

    void Start()
    {
        animacionPuertaVerja = verja.GetComponent<Animator>();
        recordarLlave = GameObject.Find("RecordatorioLlave");

        recordarLlave.SetActive(false);

        tieneLlave = false;
        llaveUsada = false;
        antiSpam = false;

        brillitoLlave.SetActive(false);

        typewriter = gameObject.AddComponent<TypewriterEffect>(); // Añadido
        recordarLlaveTMP.gameObject.SetActive(false);             // Añadido

        // Aseguramos que las bandas estén fuera de la pantalla al principio (por encima y por debajo)
        bandaArriba.anchoredPosition = new Vector2(0, Screen.height); // Fuera de la pantalla arriba
        bandaAbajo.anchoredPosition = new Vector2(0, -Screen.height); // Fuera de la pantalla abajo

        bandaArriba.gameObject.SetActive(false);  // Bandas desactivadas al inicio
        bandaAbajo.gameObject.SetActive(false);   // Bandas desactivadas al inicio
    }

    public override void Interact()
    {
        if (!llaveUsada && !antiSpam)
        {
            if (tieneLlave)
            {
                animacionPuertaVerja.SetBool("usaLlave", true);
                StartCoroutine(AnimarBandasYEliminarVerja());  // Llamada a la animación de las bandas negras

                imagenLlave.SetActive(false);
                llaveUsada = true;
            }
            else
            {
                llaveVerja.SetActive(true);

                StartCoroutine(Recordatorio());
            }
        }
    }

    IEnumerator BorrarPuerta()
    {
        yield return new WaitForSeconds(3f);

        Destroy(this.gameObject);
        Destroy(verja);
        Destroy(muroVerja);
    }

    IEnumerator Recordatorio()
    {
        antiSpam = true;

        typewriter.MostrarTexto(recordarLlaveTMP); // Reemplaza el activar/desactivar

        yield return new WaitForSeconds(5f); // Ajustado ligeramente para que dé tiempo al texto

        brillitoLlave.SetActive(true);

        antiSpam = false;
    }

    IEnumerator AnimarBandasYEliminarVerja()
    {
        // Activamos las bandas negras al iniciar la animación
        bandaArriba.gameObject.SetActive(true);
        bandaAbajo.gameObject.SetActive(true);

        float tiempoAnimacion = 1f;

        // Banda superior (de fuera de la pantalla hacia la posición Y deseada)
        Vector2 posicionInicialArriba = bandaArriba.anchoredPosition;
        Vector2 posicionFinalArriba = new Vector2(0, yPosBandaArriba); // Posición final en Y para la banda superior
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < tiempoAnimacion)
        {
            bandaArriba.anchoredPosition = Vector2.Lerp(posicionInicialArriba, posicionFinalArriba, tiempoTranscurrido / tiempoAnimacion);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        bandaArriba.anchoredPosition = posicionFinalArriba;

        // Banda inferior (de fuera de la pantalla hacia la posición Y deseada)
        Vector2 posicionInicialAbajo = bandaAbajo.anchoredPosition;
        Vector2 posicionFinalAbajo = new Vector2(0, yPosBandaAbajo); // Posición final en Y para la banda inferior
        tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < tiempoAnimacion)
        {
            bandaAbajo.anchoredPosition = Vector2.Lerp(posicionInicialAbajo, posicionFinalAbajo, tiempoTranscurrido / tiempoAnimacion);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        bandaAbajo.anchoredPosition = posicionFinalAbajo;

        // Ahora borramos la puerta
        yield return new WaitForSeconds(1f);  // Tiempo para mantener las bandas antes de eliminar la puerta
        StartCoroutine(BorrarPuerta());

        // Desactivar las bandas negras después de la animación
        yield return new WaitForSeconds(1f); // Esperar después de la animación de la puerta
        bandaArriba.gameObject.SetActive(false);
        bandaAbajo.gameObject.SetActive(false);
    }
}
