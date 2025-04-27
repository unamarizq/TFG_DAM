using UnityEngine;
using UnityEngine.InputSystem; // Para usar Gamepad

public class DisparoBolaDeFuego : MonoBehaviour
{
    public GameObject bolaDeFuegoPrefab;
    public GameObject particulasPrefab; // 🎇 Prefab de partículas para el disparo
    public Transform puntoDeDisparo;
    public float fuerzaDisparo = 8f;
    public AudioClip sonidoDisparo;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private Vector3 posicionInicialPuntoDisparo;
    private bool ultimaDireccionFlipX; // Para detectar cambios de dirección

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        posicionInicialPuntoDisparo = puntoDeDisparo.localPosition;
        ultimaDireccionFlipX = spriteRenderer.flipX; // Guardamos cómo empieza
    }

    private void Update()
    {
        // Disparo con teclado (F) o con mando (botón East)
        if (Input.GetKeyDown(KeyCode.F) || (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame))
        {
            LanzarBolaDeFuego();
        }

        // Solo actualizamos el punto de disparo si cambia la dirección
        if (spriteRenderer.flipX != ultimaDireccionFlipX)
        {
            ActualizarPuntoDeDisparo();
            ultimaDireccionFlipX = spriteRenderer.flipX;
        }
    }

    void LanzarBolaDeFuego()
    {
        GameObject bola = Instantiate(bolaDeFuegoPrefab, puntoDeDisparo.position, Quaternion.identity);
        Rigidbody2D rb = bola.GetComponent<Rigidbody2D>();
        SpriteRenderer bolaSprite = bola.GetComponent<SpriteRenderer>();

        if (rb != null)
        {
            float direccion = spriteRenderer.flipX ? -1f : 1f;
            rb.velocity = new Vector2(direccion * Mathf.Abs(fuerzaDisparo), 0f);
        }

        if (bolaSprite != null)
        {
            bolaSprite.flipX = spriteRenderer.flipX;
        }

        if (sonidoDisparo != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoDisparo);
        }

        // 🎇 Instanciar las partículas justo donde dispara
        if (particulasPrefab != null)
        {
            GameObject particulas = Instantiate(particulasPrefab, puntoDeDisparo.position, Quaternion.identity);
            Destroy(particulas, 1f); // 🧹 Destruir las partículas después de 1 segundo
        }
    }

    void ActualizarPuntoDeDisparo()
    {
        if (spriteRenderer.flipX)
        {
            puntoDeDisparo.localPosition = new Vector3(-Mathf.Abs(posicionInicialPuntoDisparo.x), posicionInicialPuntoDisparo.y, posicionInicialPuntoDisparo.z);
        }
        else
        {
            puntoDeDisparo.localPosition = new Vector3(Mathf.Abs(posicionInicialPuntoDisparo.x), posicionInicialPuntoDisparo.y, posicionInicialPuntoDisparo.z);
        }
    }
}
