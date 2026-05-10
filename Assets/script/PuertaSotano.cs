using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaSotano : MonoBehaviour
{
    public string escenaSotano = "Sotano";
    
    [Header("Referencia al Mensaje")]
    // Arrastra aquí el objeto que tiene el script MensajeElegante
    public MensajePuerta scriptMensaje; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ControladorGlobal.tieneLlave == true) 
            {
                ControladorGlobal.puntoAparicion = 3;
                SceneManager.LoadScene(escenaSotano);
            } 
            else 
            {
                Debug.Log("Está cerrado... necesito buscar la llave en la caja.");
                
                // SI EL SCRIPT EXISTE, LANZAMOS EL MENSAJE SUAVE
                if (scriptMensaje != null)
                {
                    scriptMensaje.LanzarMensaje();
                }
            }
        }
    }
}