using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosZombies : MonoBehaviour
{
    // Variables
    public AudioClip sonidoZombi1;
    public AudioClip sonidoZombi2;
    public AudioClip sonidoZombi3;
    public AudioClip sonidoZombi4;
    public AudioClip sonidoZombi5;
    AudioSource audioFuente;

    private List<AudioClip> listaSonidosZombi = new List<AudioClip>();

    public float intervaloMin = 1f;
    public float intervaloMax = 5f;

    void Start()
    {
        // Llenar la lista de sonidos
        listaSonidosZombi.Add(sonidoZombi1);
        listaSonidosZombi.Add(sonidoZombi2);
        listaSonidosZombi.Add(sonidoZombi3);
        listaSonidosZombi.Add(sonidoZombi4);
        listaSonidosZombi.Add(sonidoZombi5);

        audioFuente = this.GetComponent<AudioSource>();

        // Iniciar la corrutina
        StartCoroutine(SonidoZombiAleatorio());
    }

    // Esta corrutina inicia un clip aleatorio de zombie cada ciertos segundos (valor aleatorio de 1 a 5)
    IEnumerator SonidoZombiAleatorio()
    {
        while (true)
        {
            // Rango aleatorio de tiempo mínimo y máximo
            float tiempoEspera = Random.Range(intervaloMin, intervaloMax);
            yield return new WaitForSeconds(tiempoEspera);

            // Se escoge un sonido aleatorio de la lista
            int indice = Random.Range(0, listaSonidosZombi.Count);
            AudioClip sonidoSeleccionado = listaSonidosZombi[indice];

            // Reproducir el sonido
            audioFuente.PlayOneShot(sonidoSeleccionado);
        }
    }
}
