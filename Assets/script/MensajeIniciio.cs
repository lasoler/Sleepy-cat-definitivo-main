using UnityEngine;

public class MostrarImagen : MonoBehaviour
{
    public GameObject imagen;
    public float tiempoEspera = 2f;  
    public float segundos = 10f;    

    void Start()
    {
        // Si el mensaje ya se mostró en otra ocasión, destruimos o apagamos el cartel
        if (ControladorGlobal.mensajeInicialMostrado == true)
        {
            imagen.SetActive(false);
            return; 
        }
       
        imagen.SetActive(false);
     
        // Cambiamos el Invoke antiguo por uno que llama al nuevo método con Fade
        Invoke("MostrarConFade", tiempoEspera);

        ControladorGlobal.mensajeInicialMostrado = true;
    }

    void MostrarConFade()
    {
        if (imagen != null)
        {
            // Le pedimos al GameManager que muestre esta imagen con su transición suave
            GameManager.Instance.MostrarImagenMensaje(imagen, segundos);
        }
    }
}