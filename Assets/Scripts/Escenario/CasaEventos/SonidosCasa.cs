using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosCasa : Interactable
{
   

    private bool spamSonido = false;

    public AudioClip sonido;

    AudioSource audioFuente;



    public void Start()
    {
        
        audioFuente = this.GetComponent<AudioSource>();

    }

    public override void Interact()
    {
        

        if (!spamSonido)
        {

            spamSonido = true;

            audioFuente.PlayOneShot(sonido);


            StartCoroutine(ResetAntiSpam());
            
        }



    }


    private IEnumerator ResetAntiSpam()
    {
        yield return new WaitForSeconds(3f);
        spamSonido = false;
    }
}
