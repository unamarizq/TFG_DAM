using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SeguirJugadorArea : MonoBehaviour
{
    public float radioBusqueda;
    public LayerMask capaJugador;
    public Transform transformJugador;
    public float velocidadMovimiento;
    public float distanciaMaxima;
    private Vector3 puntoInicial;
    public EstadosMovimiento estadoActual;
    public Animator anim;
    public bool mirandoDerecha;
    private bool estaMuerto = false; // Evita múltiples activaciones
    private AudioSource audioSource;
    

    public enum EstadosMovimiento
    {
        Esperando,
        Siguiendo,
        Volviendo,
    }

    private void Start()
    {
        puntoInicial = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (estaMuerto) return;

        switch (estadoActual)
        {
            case EstadosMovimiento.Esperando:
                EstadoEsperando();
                break;
            case EstadosMovimiento.Siguiendo:
                EstadoSiguiendo();
                break;
            case EstadosMovimiento.Volviendo:
                EstadoVolviendo();
                break;
        }
    }

    private void EstadoEsperando()
    {
        anim.SetBool("Volar", false);
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);

        if (jugadorCollider)
        {
            transformJugador = jugadorCollider.transform;
            estadoActual = EstadosMovimiento.Siguiendo;
        }
    }

    private void EstadoSiguiendo()
    {
        if (transformJugador == null)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            return;
        }
        anim.SetBool("Volar", true);

        // Mover hacia el jugador manteniendo la posición en Z
        Vector3 nuevaPosicion = Vector2.MoveTowards(transform.position, transformJugador.position, velocidadMovimiento * Time.deltaTime);
        transform.position = new Vector3(nuevaPosicion.x, nuevaPosicion.y, transform.position.z);

        GirarAObjetivo(transformJugador.position);

        // Cambiar a "Volviendo" si está fuera del rango máximo desde el punto inicial
        if (Vector2.Distance(transform.position, puntoInicial) > distanciaMaxima)
        {
            estadoActual = EstadosMovimiento.Volviendo;
            transformJugador = null;
        }
    }

    private void EstadoVolviendo()
    {
        anim.SetBool("Volar", true);
        // Mover hacia el punto inicial manteniendo la posición en Z
        Vector3 nuevaPosicion = Vector2.MoveTowards(transform.position, puntoInicial, velocidadMovimiento * Time.deltaTime);
        transform.position = new Vector3(nuevaPosicion.x, nuevaPosicion.y, transform.position.z);

        GirarAObjetivo(puntoInicial);

        // Volver a "Esperando" si está lo suficientemente cerca del punto inicial
        if (Vector2.Distance(transform.position, puntoInicial) < 0.1f)
        {
            estadoActual = EstadosMovimiento.Esperando;
        }
    }

    private void GirarAObjetivo(Vector3 objetivo)
    {
        if (objetivo.x > transform.position.x && !mirandoDerecha)
        {
            Girar();
        }
        else if (objetivo.x < transform.position.x && mirandoDerecha)
        {
            Girar();
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
{
    if (estaMuerto || anim == null) return; // Evita errores si anim no está asignado

    if (collision.CompareTag("Personaje"))
    {
        float posicionJugadorY = collision.transform.position.y;
        float posicionEnemigoY = transform.position.y;

        if (posicionJugadorY > posicionEnemigoY + 0.3f) // Verifica si el jugador viene desde arriba
        {
            estaMuerto = true; // Evita que vuelva a ejecutarse
            
            if (anim != null) 
                anim.SetTrigger("Muerto"); // Activa la animación de muerte

            // Rebote del jugador
            Rigidbody2D rbJugador = collision.GetComponent<Rigidbody2D>();
            if (rbJugador != null)
            {
                rbJugador.velocity = new Vector2(rbJugador.velocity.x, 7f);
            }

            // Obtener duración del clip de animación
            float duracionAnimacion = 0.5f; 
            if (anim != null && anim.GetCurrentAnimatorClipInfo(0).Length > 0)
            {
                duracionAnimacion = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            }

            duracionAnimacion += 0.35f; // Tiempo extra para asegurar la animación

            StartCoroutine(DestruirDespuesDeTiempo(duracionAnimacion));
        }
    }
}*/

private void OnTriggerEnter2D(Collider2D collision)
{
    if (estaMuerto || anim == null) return;

    if (collision.CompareTag("Personaje"))
    {
        if (collision.gameObject.name == "Personaje") 
        {
            Debug.Log("Colisión con la cabeza");
            ReiniciarEscena(); 
            
        }
        else 
        {
            float posicionJugadorY = collision.transform.position.y;
            float posicionEnemigoY = transform.position.y;

            if (posicionJugadorY > posicionEnemigoY + 0.3f) 
            {
                estaMuerto = true;
                anim.SetTrigger("Muerto");

                audioSource.Play();
                

                Rigidbody2D rbJugador = collision.GetComponent<Rigidbody2D>();
                if (rbJugador != null)
                {
                    rbJugador.velocity = new Vector2(rbJugador.velocity.x, 7f);
                }

                float duracionAnimacion = anim.GetCurrentAnimatorClipInfo(0).Length > 0 
                    ? anim.GetCurrentAnimatorClipInfo(0)[0].clip.length + 0.35f 
                    : 0.85f;

                StartCoroutine(DestruirDespuesDeTiempo(duracionAnimacion));
            }
            else
            {
                ReiniciarEscena();
            }
            
        }
    }
}


private IEnumerator DestruirDespuesDeTiempo(float tiempo)
{
    yield return new WaitForSeconds(tiempo);
    Destroy(gameObject);
}

private void ReiniciarEscena()
{  
    Zafiro.contadorZafiros--;

    if (Zafiro.contadorZafiros <= 0)
    {
        SceneManager.LoadScene("MenuPrincipal");
        Zafiro.contadorZafiros = 3;
    }
    else
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

// private void OnDrawGizmos(){
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);
        // }


}





        

    

