using UnityEngine;
using System.Collections;

public class gestorInventario : MonoBehaviour
{
    public CanvasGroup iconoLlaveCG; // Arrastra aquí el icono (que ahora tiene el Canvas Group)
    public float velocidadFade = 2f;

    void Start()
    {
        // Comprobamos si el gato ya tiene la llave en el "Cerebro" global
        if (ControladorGlobal.tieneLlave)
        {
            // Iniciamos la aparición suave
            StartCoroutine(AparecerIcono());
        }
        else
        {
            // Si no la tiene, nos aseguramos de que sea invisible
            iconoLlaveCG.alpha = 0;
        }
    }

    IEnumerator AparecerIcono()
    {
        // Mientras no sea totalmente opaco...
        while (iconoLlaveCG.alpha < 1)
        {
            // Subimos el alpha poco a poco
            iconoLlaveCG.alpha += Time.deltaTime * velocidadFade;
            yield return null; // Esperamos al siguiente frame
        }
    }
}