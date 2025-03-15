using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;  // Necesario para acceder a Gamepad

public class Palanca : MonoBehaviour
{
    private bool dentroDelRango = false;
    public Transform pinchos; // Asigna los pinchos en el Inspector
    public float desplazamiento = -2f; // Distancia hacia abajo
    public float velocidadBajada = 2f;
    private bool activado = false;

    // Referencias para los Sprites
    public Sprite imagenNormal; // La imagen de la palanca cuando está inactiva
    public Sprite imagenActivada; // La imagen de la palanca cuando está activada
    private SpriteRenderer spriteRenderer; // Para cambiar la imagen

    private void Start()
    {
        // Obtener el componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Personaje")) // Asegúrate de que el jugador tiene esta etiqueta
        {
            dentroDelRango = true;
            Debug.Log("Jugador dentro del rango de la palanca");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Personaje"))
        {
            dentroDelRango = false;
            Debug.Log("Jugador salió del rango de la palanca");
        }
    }

    private void Update()
    {
        if ((dentroDelRango && Input.GetKeyDown(KeyCode.E) && !activado) || 
            (dentroDelRango && Gamepad.current.buttonWest.wasPressedThisFrame && !activado))
        {
            Debug.Log("Palanca activada con la tecla E o botón X del mando");
            StartCoroutine(BajarPinchos());
            activado = true;

            // Cambiar la imagen de la palanca
            spriteRenderer.sprite = imagenActivada;

            // Llamar a la función para hacer vibrar el mando
            HacerVibrarMando();
        }
    }

    private IEnumerator BajarPinchos()
    {
        Vector3 posicionInicial = pinchos.position;
        Vector3 posicionFinal = new Vector3(posicionInicial.x, posicionInicial.y + desplazamiento, posicionInicial.z);

        float tiempo = 0f;
        while (tiempo < 1f)
        {
            pinchos.position = Vector3.Lerp(posicionInicial, posicionFinal, tiempo);
            tiempo += Time.deltaTime * velocidadBajada;
            yield return null;
        }
        pinchos.position = posicionFinal;
    }

    private void HacerVibrarMando()
    {
        // Comprobar si hay un mando conectado
        if (Gamepad.current != null)
        {
            // Vibrar el mando durante 0.2 segundos con una intensidad de 0.5
            Gamepad.current.SetMotorSpeeds(0.5f, 0.5f);
            Invoke("DetenerVibracion", 0.2f); // Detener la vibración después de 0.2 segundos
        }
    }

    private void DetenerVibracion()
    {
        // Detener la vibración
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }
}
