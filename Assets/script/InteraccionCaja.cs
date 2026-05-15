using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ParpadeoCajaSimple : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color colorOriginal;
    
    public Color colorBrillo = Color.yellow;
    public float velocidad = 5f; 
    public GameObject canvasTexto; // El "Pulsa E"
    [SerializeField] private string NombreEscena;
    private bool estaCerca = false;

    [Header("Imágenes de Mensajes (UI)")]
    // Arrastra aquí la imagen de "Necesitas buscar 4 llaves"
    public GameObject imagenNecesitasLlaves; 
    // Arrastra aquí la imagen de "Aún te faltan llaves" (opcional, si tienes una genérica)
    public GameObject imagenFaltanLlaves; 

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        colorOriginal = sr.color;
        if (canvasTexto != null) canvasTexto.SetActive(false);
    }

    void Update()
    {
        if (estaCerca)
        {
            float oscilacion = Mathf.Sin(Time.time * velocidad);
            if (oscilacion > 0) { sr.color = colorBrillo; } 
            else { sr.color = colorOriginal; }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                EvaluarInteraccionCaja();
            }
        }
    }

    private void EvaluarInteraccionCaja()
    {
        // CASO 1: Primera vez que viene
        if (!GameManager.Instance.haVistoMensajeCaja)
        {
            GameManager.Instance.haVistoMensajeCaja = true;
            // Le mandamos su imagen al GameManager para que la encienda por 4 segundos
            GameManager.Instance.MostrarImagenMensaje(imagenNecesitasLlaves, 4f);
            return;
        }

        // CASO 2: Ya vio el mensaje pero no tiene las 4 llaves
        if (GameManager.Instance.llavesActuales < GameManager.Instance.llavesTotalesNecesarias)
        {
            GameManager.Instance.MostrarImagenMensaje(imagenFaltanLlaves, 3f);
            return;
        }

        // CASO 3: Tiene las 4 llaves, carga el minijuego
        if (GameManager.Instance.llavesActuales >= GameManager.Instance.llavesTotalesNecesarias)
        {
            SceneManager.LoadScene(NombreEscena);
        }
    }

    // (Tus funciones OnTriggerEnter2D y OnTriggerExit2D se quedan exactamente igual)
    private void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("Player")) { estaCerca = true; if (canvasTexto != null) canvasTexto.SetActive(true); } }
    private void OnTriggerExit2D(Collider2D other) { if (other.CompareTag("Player")) { estaCerca = false; sr.color = colorOriginal; if (canvasTexto != null) canvasTexto.SetActive(false); } }
}

