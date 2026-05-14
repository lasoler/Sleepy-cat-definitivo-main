using UnityEngine;

public class MensajePuerta : MonoBehaviour
{
    public float tiempoVisible = 3f; // Cuánto tiempo se queda arriba

    // Esta función la llamas desde el script de interacción de la puerta o un Trigger
    public void LanzarMensaje()
    {
        // Enviamos este mismo objeto al GameManager para que le aplique el Fade suave
        GameManager.Instance.MostrarImagenMensaje(this.gameObject, tiempoVisible);
    }
}