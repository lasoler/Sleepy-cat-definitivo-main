using UnityEngine;
using UnityEngine.SceneManagement; // Importante para cambiar escenas

public class CambioEscena : MonoBehaviour
{
    [SerializeField] private string nombreEscena; // Escribe aquí el nombre de la siguiente escena

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobamos si lo que entró en el trigger es el jugador
        if (collision.CompareTag("Player"))
        {
            ControladorGlobal.puntoAparicion = 2;
            SceneManager.LoadScene(nombreEscena);
        }
    }
}