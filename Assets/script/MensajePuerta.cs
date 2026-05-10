using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MensajePuerta : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float velocidadFade = 2f; // Qué tan rápido aparece/desaparece
    public float tiempoVisible = 3f; // Cuánto tiempo se queda arriba

    void Awake()
    {
        // Buscamos el componente Canvas Group en este mismo objeto
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0; // Empezamos invisible
    }

    // Esta función la puedes llamar desde otros scripts o con un Trigger
    public void LanzarMensaje()
    {
        StartCoroutine(EfectoMensaje());
    }

    IEnumerator EfectoMensaje()
    {
        // 1. FADE IN (Aparecer)
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * velocidadFade;
            yield return null;
        }

        // 2. ESPERA
        yield return new WaitForSeconds(tiempoVisible);

        // 3. FADE OUT (Desaparecer)
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * velocidadFade;
            yield return null;
        }
    }
}