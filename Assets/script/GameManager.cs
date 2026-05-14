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
    // Arrastra aquí las 4 imágenes de las llaves (deben tener CanvasGroup)
    public GameObject[] iconosLlaves; 
    public float velocidadFade = 2f; 

    // Referencia interna para el mensaje que se esté mostrando actualmente
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
        // Al empezar, ocultamos todas las llaves de la esquina
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

    // --- LÓGICA DE LAS LLAVES (ESQUINA) ---

    public void RecogerLlave()
    {
        if (llavesActuales < llavesTotalesNecesarias)
        {
            // Seleccionamos la imagen que corresponde a la llave actual
            GameObject llaveAActivar = iconosLlaves[llavesActuales];
            llavesActuales++;

            // Iniciamos su aparición suave
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

    // --- LÓGICA DE MENSAJES (CENTRO) ---

    public void MostrarImagenMensaje(GameObject imagenAMostrar, float duracion)
    {
        if (imagenAMostrar == null) return;

        // Si ya hay un mensaje en pantalla, lo cortamos para mostrar el nuevo
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

        // FADE IN
        while (cg.alpha < 1f)
        {
            cg.alpha += Time.deltaTime * velocidadFade;
            yield return null;
        }
        cg.alpha = 1f;

        // ESPERA
        yield return new WaitForSeconds(duracion);

        // FADE OUT
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