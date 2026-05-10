using UnityEngine;
using UnityEngine.InputSystem;
public class movPersonajeMinijuego : MonoBehaviour
{




 public float velocidad = 0.5f;

 public float impulsoSalto= 10.0f;


public Vector3 inicioPersonaje = new Vector3 (1,2,3);
Animator controlAnimacion;

Rigidbody2D rb;

bool puedoSaltar = false;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     this.transform.position = inicioPersonaje;
     controlAnimacion = GetComponent<Animator>();
     rb = GetComponent<Rigidbody2D>();





   
    }




    // Update is called once per frame
    void Update()
    {
       
      Vector2 moveInput = InputSystem.actions["Move"].ReadValue<Vector2>();
     
      this.transform.Translate(moveInput.x * velocidad, moveInput.y * velocidad, 0);
     
      if(moveInput.x < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        } else if (moveInput.x > 0)
        {
           this.GetComponent<SpriteRenderer>().flipX = false;

        }

     if (moveInput.x != 0)
        {
            controlAnimacion.SetBool("activaCamina", true);
        }
        else
        {
            controlAnimacion.SetBool("activaCamina", false);
        }

     //SALTO


         bool salto = InputSystem.actions["Jump"]. WasPressedThisFrame();
         Debug.Log(salto);
    if(salto == true)
         {
            Debug.Log("Salto");
            rb.AddForce(transform.up * impulsoSalto, ForceMode2D.Impulse);
           
         }
     RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * 0.5f);

     if(hit.collider == true)
        {
            puedoSaltar = true;
        }
        else
        {
            puedoSaltar = false;
     
        }



    }
}
