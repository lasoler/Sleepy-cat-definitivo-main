using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoriaMinijuego : MonoBehaviour
{
    public string escenaPrincipal = "casa"; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
        
            ControladorGlobal.tieneLlave = true;
           
            Debug.Log("Variable tieneLlave activada: " + ControladorGlobal.tieneLlave);

            ControladorGlobal.puntoAparicion = 1;
            
            
            SceneManager.LoadScene(escenaPrincipal);
        }
    }
}