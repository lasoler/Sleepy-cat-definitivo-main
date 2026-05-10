using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // Necesario para detectar cambios de escena

public class movpersonaje : MonoBehaviour
{
    public float velocidad = 0.5f;
    public Vector3 inicioPersonaje = new Vector3(1, 2, 0); // Z siempre en 0

    Animator animator;
    SpriteRenderer sr;

    void Awake()
    {
        GameObject[] objetosPersonaje = GameObject.FindGameObjectsWithTag("Player");

        if (objetosPersonaje.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
        // Llamamos a la función de posicionar por primera vez
        PosicionarGato();
    }

    // Como usas DontDestroyOnLoad, necesitamos esto para que el gato se mueva 
    // CADA VEZ que una escena nueva termine de cargar
    private void OnEnable() { SceneManager.sceneLoaded += AlCargarEscena; }
    private void OnDisable() { SceneManager.sceneLoaded -= AlCargarEscena; }

    void AlCargarEscena(Scene scene, LoadSceneMode mode)
    {
        PosicionarGato();
    }

    void PosicionarGato()
    {
        // 1. Buscamos si hay un punto especial según el ControladorGlobal
        if (ControladorGlobal.puntoAparicion == 1)
        {
            GameObject spawn = GameObject.FindWithTag("spawnCaja");
            if (spawn != null) inicioPersonaje = spawn.transform.position;
        }
        else if (ControladorGlobal.puntoAparicion == 2)
        {
            GameObject spawn = GameObject.FindWithTag("spawnSotano");
            if (spawn != null) inicioPersonaje = spawn.transform.position;
        }

        else if (ControladorGlobal.puntoAparicion == 3)
        {
            GameObject spawn = GameObject.FindWithTag("spawnEntradaSotano");
            if (spawn != null) inicioPersonaje = spawn.transform.position;
        }

        // 2. Aplicamos la posición (Esto debe ir AL FINAL para que use el valor actualizado)
        this.transform.position = new Vector3(inicioPersonaje.x, inicioPersonaje.y, 0);
        
        // 3. Reset de seguridad: Si quieres que al entrar a otra puerta no use el mismo spawn
        // ControladorGlobal.puntoAparicion = 0; 
    }

    void Update()
    {
        Vector2 moveInput = InputSystem.actions["Move"].ReadValue<Vector2>();
        this.transform.Translate(moveInput.x * velocidad, moveInput.y * velocidad, 0);

        if (moveInput.x < 0) sr.flipX = true;
        else if (moveInput.x > 0) sr.flipX = false;

        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
        animator.SetBool("IsMoving", moveInput.sqrMagnitude > 0.01f);
    }
}