using UnityEngine;

public class MostrarImagen : MonoBehaviour
{
    public GameObject imagen;
    public float tiempoEspera = 2f;   
    public float segundos = 10f;     
    

    void Start()
    {
       if (ControladorGlobal.mensajeInicialMostrado == true)
        {
            imagen.SetActive(false);
            return; 
        } 
        
        imagen.SetActive(false);
      
        Invoke("Mostrar", tiempoEspera);

        ControladorGlobal.mensajeInicialMostrado = true;
    }

    void Mostrar()
    {
        imagen.SetActive(true);

     
        Invoke("Ocultar", segundos);
    }

    void Ocultar()
    {
        imagen.SetActive(false);

        
     
    }
}