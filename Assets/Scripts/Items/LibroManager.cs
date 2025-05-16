using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LibroManager : MonoBehaviour
{
    public TextMeshProUGUI libroTextoTMP;
    public float distanciaInteraccion = 2f;
    public int totalLibros = 8;

    private int librosRecogidos = 0;
    private bool textoActivo = false;

    void Start()
    {
        libroTextoTMP.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject libroCercano = BuscarLibroCercano();

            if (libroCercano != null)
            {
                var libroScript = libroCercano.GetComponent<LibroInteractuable>();
                if (libroScript != null && !libroScript.recogido)
                {
                    libroScript.recogido = true;
                    librosRecogidos++;
                    libroCercano.SetActive(false);
                    libroTextoTMP.text = $"{librosRecogidos}/{totalLibros}";

                    if (!textoActivo)
                    {
                        libroTextoTMP.gameObject.SetActive(true);
                        textoActivo = true;
                    }

                    if (librosRecogidos >= totalLibros)
                    {
                        libroTextoTMP.gameObject.SetActive(false);
                        textoActivo = false;
                    }
                }
            }
        }
    }

    GameObject BuscarLibroCercano()
    {
        GameObject[] libros = GameObject.FindGameObjectsWithTag("Libro");
        foreach (GameObject libro in libros)
        {
            float distancia = Vector3.Distance(transform.position, libro.transform.position);
            if (distancia <= distanciaInteraccion)
            {
                var libroScript = libro.GetComponent<LibroInteractuable>();
                if (libroScript != null && !libroScript.recogido)
                    return libro;
            }
        }
        return null;
    }
}