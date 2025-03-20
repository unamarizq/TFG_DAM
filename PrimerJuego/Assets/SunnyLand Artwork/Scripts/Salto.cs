using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;

public class Salto : MonoBehaviour
{
    public Rigidbody2D rb;
    public float fuerza;
    public Animator anim;
    private bool Suelo;

    public UnityEvent OnLandEvent;

    public float distanciaSuelo = 1.0f; // Distancia para el raycast
    public LayerMask capaSuelo; // Para asegurarnos de que solo detecte el suelo

     private bool aumentoSaltoActivo = false;  // Controla si el salto ha sido aumentado
    private float duracionAumento = 10f; // Duración del aumento de salto en segundos

    private void Start()
    {
        Suelo = true; // Al inicio estamos en el suelo
    }

    void Update()
    {
        // Verificamos si estamos presionando la tecla de salto y si estamos tocando el suelo
        if ((Input.GetKeyDown(KeyCode.Space) || (Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame)) && Suelo)
        {
            // Aplicamos fuerza en Y para saltar
            rb.velocity = new Vector2(rb.velocity.x, fuerza);
            Suelo = false;
            anim.SetBool("Jump", true);
        }

        // Actualizamos la animación de acuerdo a si estamos en el suelo
        anim.SetBool("OnGround", Suelo);
        anim.SetFloat("YVelocity", rb.velocity.y);
    }

    void FixedUpdate()
{
    // Raycast para detectar si tocamos el suelo
    Vector2 raycastOrigen = new Vector2(transform.position.x, transform.position.y - 0.7f);

    RaycastHit2D hit = Physics2D.Raycast(raycastOrigen, Vector2.down, distanciaSuelo);
    Debug.DrawRay(raycastOrigen, Vector2.down * distanciaSuelo, Color.red);

    if (hit.collider != null && hit.collider.CompareTag("Suelo"))
    {
        if (!Suelo)
        {
            Suelo = true;
            anim.SetBool("Jump", false);

            if (OnLandEvent != null)
                OnLandEvent.Invoke();
        }
    }
    else
    {
        Suelo = false;
    }

    // Raycasts laterales 
float distanciaPared = 0.1f; 
Vector2 origenIzquierda = new Vector2(transform.position.x - 0.4f, transform.position.y);
Vector2 origenDerecha = new Vector2(transform.position.x + 0.4f, transform.position.y);

// Modificamos la posición para que estén más hacia abajo
Vector2 origenIzquierdaAbajo = new Vector2(transform.position.x - 0.4f, transform.position.y - 0.4f); 
Vector2 origenDerechaAbajo = new Vector2(transform.position.x + 0.4f, transform.position.y - 0.4f); 

RaycastHit2D hitIzquierda = Physics2D.Raycast(origenIzquierda, Vector2.left, distanciaPared);
RaycastHit2D hitDerecha = Physics2D.Raycast(origenDerecha, Vector2.right, distanciaPared);

RaycastHit2D hitIzquierdaAbajo = Physics2D.Raycast(origenIzquierdaAbajo, Vector2.left, distanciaPared);
RaycastHit2D hitDerechaAbajo = Physics2D.Raycast(origenDerechaAbajo, Vector2.right, distanciaPared);

Debug.DrawRay(origenIzquierda, Vector2.left * distanciaPared, Color.blue);
Debug.DrawRay(origenDerecha, Vector2.right * distanciaPared, Color.green);

Debug.DrawRay(origenIzquierdaAbajo, Vector2.left * distanciaPared, Color.cyan); // Dibujar el nuevo raycast
Debug.DrawRay(origenDerechaAbajo, Vector2.right * distanciaPared, Color.magenta); // Dibujar el nuevo raycast

// Si detecta una pared a la izquierda o derecha o hacia abajo, evita que el personaje se quede pegado
if ((hitIzquierda.collider != null && hitIzquierda.collider.CompareTag("Suelo")) ||
    (hitDerecha.collider != null && hitDerecha.collider.CompareTag("Suelo")) ||
    (hitIzquierdaAbajo.collider != null && hitIzquierdaAbajo.collider.CompareTag("Suelo")) ||
    (hitDerechaAbajo.collider != null && hitDerechaAbajo.collider.CompareTag("Suelo")))
{
    rb.velocity = new Vector2(0, rb.velocity.y);
}


}



    // Detección de colisiones con el suelo
    public void OnCollisionEnter2D(Collision2D laCosa)
    {
        if (laCosa.gameObject.tag == "Suelo")
        {
            anim.SetBool("Jump", false);
            Suelo = true;

            // Llamamos al evento cuando toca el suelo
            if (OnLandEvent != null)
                OnLandEvent.Invoke();
        }
    }

    // Método para comprobar si se debe aumentar el salto
    public void ComprobarSalto()
    {
        if (Cristales.contadorCristales >= 10 && !aumentoSaltoActivo)
        {
            aumentoSaltoActivo = true; // Activamos el aumento de salto
            fuerza *= 1.25f; // Doblamos la fuerza de salto
             Debug.Log("Fuerza aumentada: " + fuerza);
            StartCoroutine(AumentoSalto());
            //Cristales.contadorCristales = 5;
        }
    }

    // Corutina para restaurar el salto original después de 20 segundos
    IEnumerator AumentoSalto()
    {
        yield return new WaitForSeconds(duracionAumento);
        fuerza /= 1.25f; // Restauramos la fuerza de salto a su valor original
        aumentoSaltoActivo = false; // Desactivamos el aumento de salto
    }
}
