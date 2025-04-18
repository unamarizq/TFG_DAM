using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlataformaDesaparece : MonoBehaviour
{
    public float tiempoAntesDeDesaparecer = 2f;
    public float tiempoReaparicion = 3f;
    public float tiempoDesvanecimiento = 1f;
    public float distanciaCaida = 1f;
    public float tiempoVibracion = 2f;
    public float intensidadVibracion = 0.1f;

    private Collider2D colisionador;
    private SpriteRenderer spriteRenderer;
    private Vector3 posicionInicial;
    private bool personajeEncima = false;

    void Start()
    {
        colisionador = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        posicionInicial = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Personaje") && !personajeEncima)
        {
            personajeEncima = true;
            StartCoroutine(DesaparecerYReaparecer());
        }
    }

    IEnumerator DesaparecerYReaparecer()
    {
        StartCoroutine(VibrarPlataforma());

        // Espera un poco antes de comenzar a desaparecer
        yield return new WaitForSeconds(tiempoAntesDeDesaparecer);

        // Después de este tiempo, comienza a desvanecerse y caer
        float tiempo = 0;
        Vector3 posicionOriginal = transform.position;
        Vector3 posicionDestino = posicionOriginal + Vector3.down * distanciaCaida;

        while (tiempo < tiempoDesvanecimiento)
        {
            tiempo += Time.deltaTime;
            float factor = tiempo / tiempoDesvanecimiento;

            // Aplicamos una curva exponencial para que la caída comience más rápido
            factor = Mathf.Pow(factor, 2f); // Esto hace que el factor se acelere más rápido

            // Desvanecer la plataforma
            float alpha = Mathf.Lerp(1f, 0f, factor);
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);

            // Desplazar la plataforma hacia abajo
            transform.position = Vector3.Lerp(posicionOriginal, posicionDestino, factor);

            yield return null;
        }

        // Desaparece completamente
        colisionador.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(tiempoReaparicion);

        // Reaparece la plataforma de forma gradual
        tiempo = 0;

        // Habilitar el SpriteRenderer antes de comenzar el proceso de aparición
        spriteRenderer.enabled = true;

        // Reposicionar la plataforma en su lugar original
        transform.position = posicionDestino;

        while (tiempo < tiempoDesvanecimiento)
        {
            tiempo += Time.deltaTime;
            float factor = tiempo / tiempoDesvanecimiento;

            // Aplicamos una curva exponencial para que la aparición comience más rápido
            factor = Mathf.Pow(factor, 2f);

            // Restaurar la visibilidad y la posición
            spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, factor));
            transform.position = Vector3.Lerp(posicionDestino, posicionOriginal, factor);

            yield return null;
        }

        // Asegurarse de que la plataforma esté completamente visible y en la posición correcta
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        transform.position = posicionOriginal;

        colisionador.enabled = true;
        personajeEncima = false;
    }

    IEnumerator VibrarPlataforma()
    {
        Vector3 posicionOriginal = transform.position;
        float tiempoVibracionRestante = tiempoVibracion;

        // Vibrar mientras la plataforma no ha comenzado a caer
        while (tiempoVibracionRestante > 0)
        {
            // Movimiento aleatorio hacia los lados
            transform.position = posicionOriginal + new Vector3(Random.Range(-intensidadVibracion, intensidadVibracion), 0f, 0f);

            if (Gamepad.current != null) 
            {
                Gamepad.current.SetMotorSpeeds(0.5f, 0.5f); 
            }

            tiempoVibracionRestante -= Time.deltaTime;
            yield return null;
        }

        // Restaurar la posición original después de la vibración
        transform.position = posicionOriginal;

        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }
}
