using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogoManager : MonoBehaviour
{
    public GameObject cuadroDialogo;
    public TMP_Text texto1;
    public TMP_Text texto;
    public string nombreHablante;
    [TextArea(3, 10)] public string dialogo;
    public float velocidadTexto = 0.05f;
    public float rangoInteraccion = 3f;
    public Transform jugador;

    private bool enRango = false;
    private bool dialogoActivo = false;
    private Coroutine escribirTexto;

    void Update()
    {
        // Comprobar distancia con el jugador
        enRango = Vector3.Distance(transform.position, jugador.position) <= rangoInteraccion;

        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogoActivo)
                IniciarDialogo();
            else
                CerrarDialogo();
        }
    }

    void IniciarDialogo()
    {
        dialogoActivo = true;
        cuadroDialogo.SetActive(true);
        StartCoroutine(AnimacionAparicion());
        // Bloquear movimiento del jugador
        Time.timeScale = 0f;
    }

    IEnumerator AnimacionAparicion()
    {
        cuadroDialogo.transform.localScale = Vector3.zero;
        float tiempo = 0f;
        Vector3 escalaFinal = Vector3.one;
        while (tiempo < 0.3f)
        {
            cuadroDialogo.transform.localScale = Vector3.Lerp(Vector3.zero, escalaFinal, tiempo / 0.3f);
            tiempo += Time.unscaledDeltaTime;
            yield return null;
        }
        cuadroDialogo.transform.localScale = escalaFinal;

        yield return new WaitForSecondsRealtime(1f);
        texto1.text = nombreHablante;
        texto1.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        escribirTexto = StartCoroutine(EfectoMaquinaDeEscribir(dialogo));
    }

    IEnumerator EfectoMaquinaDeEscribir(string dialogoCompleto)
    {
        texto.text = "";
        texto.gameObject.SetActive(true);
        foreach (char letra in dialogoCompleto)
        {
            texto.text += letra;
            yield return new WaitForSecondsRealtime(velocidadTexto);
        }
    }

    void CerrarDialogo()
    {
        if (escribirTexto != null) StopCoroutine(escribirTexto);
        cuadroDialogo.SetActive(false);
        dialogoActivo = false;
        Time.timeScale = 1f; // Restaurar el movimiento del jugador
    }
}