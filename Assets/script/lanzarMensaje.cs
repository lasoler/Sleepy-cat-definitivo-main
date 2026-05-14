using UnityEngine;

public class lanzarMensaje : MonoBehaviour
{
    public float tiempoVisible = 5f; // Cuánto tiempo se queda arriba al 100%

    // Esta función es la que sigue llamando tu script 'interaccionComedor'
    public void LanzarMensaje()
    {
        // Le pasamos este mismo objeto al GameManager para que controle su aparición y desaparición
        GameManager.Instance.MostrarImagenMensaje(this.gameObject, tiempoVisible);
    }
}