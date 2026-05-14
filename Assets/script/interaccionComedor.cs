using UnityEngine;
using UnityEngine.InputSystem;

public class interaccionComedor : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color colorOriginal;
   
    public Color colorBrillo = Color.yellow;
    public float velocidadBrillo = 5f; 
    public GameObject canvasTexto; // El indicador "Pulsa E"
   
    [Header("Configuración de la Imagen con Fade (LLAVE)")]
    public GameObject objetoConFade;
    private lanzarMensaje scriptFade;

    private bool estaCerca = false;
    private bool yaDioLlave = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        colorOriginal = sr.color;
       
        if (canvasTexto != null) canvasTexto.SetActive(false);
       
        if (objetoConFade != null)
        {
            scriptFade = objetoConFade.GetComponent<lanzarMensaje>();
        }
    }

    void Update()
    {
        // REQUISITO: Si no ha ido a la caja o ya dio la llave, salimos del Update inmediatamente
        // Esto evita el parpadeo y que se pueda pulsar la tecla E
        if (!GameManager.Instance.haVistoMensajeCaja || yaDioLlave) 
        {
            return; 
        }

        if (estaCerca)
        {
            // Efecto de parpadeo (Solo ocurre si haVistoMensajeCaja es true)
            float oscilacion = Mathf.Sin(Time.time * velocidadBrillo);
            sr.color = (oscilacion > 0) ? colorBrillo : colorOriginal;

            // Detecta la tecla E
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                EntregarLlave();
            }
        }
    }

    private void EntregarLlave()
    {
        // Marcamos como completado para que el Update deje de ejecutarse
        yaDioLlave = true; 
        sr.color = colorOriginal;

        if (canvasTexto != null) canvasTexto.SetActive(false);

        // Sumamos la llave y lanzamos el efecto visual
        GameManager.Instance.RecogerLlave();

        if (scriptFade != null)
        {
            scriptFade.LanzarMensaje();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Solo detectamos cercanía si el jugador ya sabe que necesita llaves
        if (other.CompareTag("Player") && GameManager.Instance.haVistoMensajeCaja && !yaDioLlave)
        {
            estaCerca = true;
            if (canvasTexto != null) canvasTexto.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            estaCerca = false;
            sr.color = colorOriginal; 
            if (canvasTexto != null) canvasTexto.SetActive(false);
        }
    }
}