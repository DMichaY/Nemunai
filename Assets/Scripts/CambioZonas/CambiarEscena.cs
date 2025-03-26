using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambiarEscena : MonoBehaviour
{
    public string nombreEscena;
    public string tagJugador = "Player";
    public Image pantallaNegra;

    private void Start()
    {
        if (pantallaNegra != null) pantallaNegra.CrossFadeAlpha(0, 2, false);
        else Debug.LogWarning("No hay pantalla negra!");
    }

    private void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag(tagJugador))
        {
            if (!string.IsNullOrEmpty(nombreEscena))
            {
                StartCoroutine("SceneLoaderCoroutine");
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