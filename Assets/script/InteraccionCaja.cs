using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class ParpadeoCajaSimple : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color colorOriginal;
    
    public Color colorBrillo = Color.yellow;
    public float velocidad = 5f; // Cuanto más alto, más rápido parpadea
    public GameObject canvasTexto;
     [SerializeField] private string NombreEscena;
    private bool estaCerca = false;

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

            if (oscilacion > 0) {
                sr.color = colorBrillo;
            } else {
                sr.color = colorOriginal;
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("Cambiando al minijuego...");
                SceneManager.LoadScene(NombreEscena);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            estaCerca = true;
        }

        if (canvasTexto != null) 
            {
                canvasTexto.SetActive(true); 
            }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            estaCerca = false;
            sr.color = colorOriginal; // Aseguramos que vuelva a su color al irnos
        }
        if (canvasTexto != null) 
            {
                canvasTexto.SetActive(false); 
            }
    }
}