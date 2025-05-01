using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JefeFinal : SeguirJugadorArea
{
    public int vida = 3;
    private bool invulnerable = false;
    private SpriteRenderer spriteRenderer;

    public Color colorDaño = Color.red;
    private Color colorOriginal;
    public AudioClip sonidoDaño;
    public AudioClip sonidoMuerte;


   private new void Start()
{
    base.Start();
    spriteRenderer = GetComponent<SpriteRenderer>();
    if (spriteRenderer != null)
        colorOriginal = spriteRenderer.color;
}

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (estaMuerto || anim == null) return;

        if (collision.CompareTag("Personaje"))
        {
            if (collision.gameObject.name == "Personaje") 
            {
                ReiniciarEscena(); 
            }
            else 
            {
                float posicionJugadorY = collision.transform.position.y;
                float posicionEnemigoY = transform.position.y;

                if (posicionJugadorY > posicionEnemigoY + 0.3f)
                {
                    // Rebote
                    Rigidbody2D rbJugador = collision.GetComponent<Rigidbody2D>();
                    if (rbJugador != null)
                        rbJugador.velocity = new Vector2(rbJugador.velocity.x, 7f);

                    RecibirGolpe();
                }
                else
                {
                    ReiniciarEscena();
                }
            }
        }

        if (collision.CompareTag("Proyectil"))
        {
            RecibirGolpe();
            Destroy(collision.gameObject); // Destruye el proyectil tras impactar
        }
    }

    private void RecibirGolpe()
{
    if (estaMuerto || invulnerable) return;

    vida--;

    if (vida <= 0)
    {
        estaMuerto = true;
        anim.SetTrigger("Muerto");
        audioSource.Play(); // Suponemos que es el sonido de muerte

        float duracionAnimacion = anim.GetCurrentAnimatorClipInfo(0).Length > 0
            ? anim.GetCurrentAnimatorClipInfo(0)[0].clip.length + 0.35f
            : 0.85f;

float tiempoTotal = duracionAnimacion + 2f;
        StartCoroutine(DestruirDespuesDeTiempo(tiempoTotal));
        //SceneManager.LoadScene("Fin");
    }
    else
    {
        anim.SetTrigger("Herido");

        // Reproducir sonido de daño
        if (sonidoDaño != null)
            audioSource.PlayOneShot(sonidoDaño);

        StartCoroutine(ParpadearInvulnerable(2f, 2f));
    }
}

    private IEnumerator ParpadearInvulnerable(float duracionParpadeo, float duracionInvulnerable)
{
    invulnerable = true;

    float tiempo = 0f;
    float intervalo = 0.1f;

    while (tiempo < duracionParpadeo)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = spriteRenderer.color == colorOriginal ? colorDaño : colorOriginal;
        }

        yield return new WaitForSeconds(intervalo);
        tiempo += intervalo;
    }

    if (spriteRenderer != null)
        spriteRenderer.color = colorOriginal;

    // Esperar duración de invulnerabilidad (si fuera mayor que la de parpadeo)
    yield return new WaitForSeconds(Mathf.Max(0, duracionInvulnerable - duracionParpadeo));

    invulnerable = false;
}
private IEnumerator DestruirDespuesDeTiempo(float tiempo)
{
    if (sonidoMuerte != null)
            audioSource.PlayOneShot(sonidoMuerte);
    yield return new WaitForSeconds(tiempo);
    SceneManager.LoadScene("Fin");
}
}
