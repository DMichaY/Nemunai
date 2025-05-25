using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TorreTeleport : MonoBehaviour
{

    public string nombreEscena;
    public string tagJugador = "Player";
    public Image pantallaNegra;
    public bool cambiarPantallaNegra;

    private void Start()
    {
        if (pantallaNegra != null) pantallaNegra.CrossFadeAlpha(0, 2, false);
        else Debug.LogWarning("No hay pantalla negra!");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (other.CompareTag(tagJugador))
            {
                if (!string.IsNullOrEmpty(nombreEscena))
                {
                    StartCoroutine("SceneLoaderCoroutine");
                }
            }

        }
    }


    public void ForceLoadScene()
    {
        StartCoroutine("SceneLoaderCoroutine");
    }



    private IEnumerator SceneLoaderCoroutine()
    {
        if (pantallaNegra != null)
        {
            if (cambiarPantallaNegra) pantallaNegra.color = Color.HSVToRGB(0, 0, 0);
            pantallaNegra.CrossFadeAlpha(1, 2, false);
        }
        else Debug.LogWarning("No hay pantalla negra!");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nombreEscena);
    }
}
