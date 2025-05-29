using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    float minZ = -80, maxZ = -12;
    GameObject player;
    public GameObject playerHealth, enemyHealth;

    private void OnEnable()
    {
        player = FindObjectOfType<KaitoFight>().gameObject;
        transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
        StartCoroutine(CinematicCamera());
    }

    private IEnumerator CinematicCamera()
    { 
        playerHealth.SetActive(false);
        enemyHealth.SetActive(false);
        yield return null;
        float t = 0;
        while(t < 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(minZ, maxZ, t));
            t += Time.deltaTime / 4;
            yield return null;
        }

        yield return new WaitForSeconds(2);

        t = 0;
        while (t < 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(maxZ, player.transform.position.z + 16, t));
            t += Time.deltaTime;
            yield return null;
        }

        playerHealth.SetActive(true);
        enemyHealth.SetActive(true);
        StartCoroutine(DynamicCamera());
    }

    private IEnumerator DynamicCamera()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(player.transform.position.z + 16, minZ, maxZ));
            yield return null;
        }
    }
}
