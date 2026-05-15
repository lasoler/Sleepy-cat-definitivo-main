using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Progreso de Llaves")]
    public int llavesActuales = 0;
    public int llavesTotalesNecesarias = 4;
    public bool haVistoMensajeCaja = false;

    [Header("Componentes de la Interfaz (UI)")]
   
    public GameObject[] iconosLlaves; 
    public float velocidadFade = 2f; 

    private Coroutine corrutinaMensajeActual;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
   
        foreach (GameObject llave in iconosLlaves)
        {
            if (llave != null)
            {
                CanvasGroup cg = llave.GetComponent<CanvasGroup>();
                if (cg == null) cg = llave.AddComponent<CanvasGroup>();
                
                cg.alpha = 0f;
                llave.SetActive(false);
            }
        }
    }


    public void RecogerLlave()
    {
        if (llavesActuales < llavesTotalesNecesarias)
        {
    
            GameObject llaveAActivar = iconosLlaves[llavesActuales];
            llavesActuales++;

   
            StartCoroutine(FadeInObjetoUI(llaveAActivar));
        }
    }

    private IEnumerator FadeInObjetoUI(GameObject obj)
    {
        obj.SetActive(true);
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();
        
        while (cg.alpha < 1f)
        {
            cg.alpha += Time.deltaTime * velocidadFade;
            yield return null;
        }
        cg.alpha = 1f;
    }

  

    public void MostrarImagenMensaje(GameObject imagenAMostrar, float duracion)
    {
        if (imagenAMostrar == null) return;

   
        if (corrutinaMensajeActual != null)
        {
            StopCoroutine(corrutinaMensajeActual);
        }

        corrutinaMensajeActual = StartCoroutine(SecuenciaFadeMensaje(imagenAMostrar, duracion));
    }

    private IEnumerator SecuenciaFadeMensaje(GameObject objeto, float duracion)
    {
        CanvasGroup cg = objeto.GetComponent<CanvasGroup>();
        if (cg == null) cg = objeto.AddComponent<CanvasGroup>();

        objeto.SetActive(true);
        cg.alpha = 0f;

     
        while (cg.alpha < 1f)
        {
            cg.alpha += Time.deltaTime * velocidadFade;
            yield return null;
        }
        cg.alpha = 1f;

  
        yield return new WaitForSeconds(duracion);


        while (cg.alpha > 0f)
        {
            cg.alpha -= Time.deltaTime * velocidadFade;
            yield return null;
        }
        cg.alpha = 0f;
        objeto.SetActive(false);

        corrutinaMensajeActual = null;
    }
}