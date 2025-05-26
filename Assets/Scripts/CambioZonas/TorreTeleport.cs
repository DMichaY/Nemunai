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
    public float startWaitTime = 0f;

    private void Start()
    {
        if (pantallaNegra != null) StartCoroutine("StartWait");
        else Debug.LogWarning("No hay pantalla negra!");
    }

    public IEnumerator StartWait()
    {
        yield return new WaitForSeconds(startWaitTime);
        pantallaNegra.CrossFadeAlpha(0, 2, false);
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
        if (pantallaNegra != null) pantallaNegra.CrossFadeAlpha(1, 2, false);
        else Debug.LogWarning("No hay pantalla negra!");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nombreEscena);
    }
}
