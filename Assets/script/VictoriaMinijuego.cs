using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoriaMinijuego : MonoBehaviour
{
    public string escenaPrincipal = "casa"; // Nombre exacto de tu mapa

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el personaje nuevo también tenga el tag Player
        {
        // ¡IMPORTANTE! Aquí es donde conseguimos la llave
            ControladorGlobal.tieneLlave = true;
           
            Debug.Log("Variable tieneLlave activada: " + ControladorGlobal.tieneLlave);

            ControladorGlobal.puntoAparicion = 1;
            
            // Volvemos a la escena de la ciudad/casa
            SceneManager.LoadScene(escenaPrincipal);
        }
    }
}