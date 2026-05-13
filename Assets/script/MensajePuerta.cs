using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MensajePuerta : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float velocidadFade = 2f; 
    public float tiempoVisible = 3f; 

    void Awake()
    {
        
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0; 
    }

    
    public void LanzarMensaje()
    {
        StartCoroutine(EfectoMensaje());
    }

    IEnumerator EfectoMensaje()
    {
      
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * velocidadFade;
            yield return null;
        }

       
        yield return new WaitForSeconds(tiempoVisible);

        
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * velocidadFade;
            yield return null;
        }
    }
}