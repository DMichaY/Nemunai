using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class IdiomaSelector : MonoBehaviour
{
    private bool activar = false;

    public void CambiarIdioma(int idioma){

        if(activar)

        return;
        
        StartCoroutine(SetIdioma(idioma));
    }
    
// seleccionar el id del idioma: 0 castellano, 1 ingles, 2 japones
    IEnumerator SetIdioma(int idiomaID){

        activar = true;

        yield return LocalizationSettings.InitializationOperation;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[idiomaID];

        activar = false;

    }
}
