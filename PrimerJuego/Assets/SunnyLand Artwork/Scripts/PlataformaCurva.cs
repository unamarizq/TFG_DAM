using UnityEngine;
using UnityEngine.InputSystem; // Necesario para la vibración
using System.Collections; // Necesario para usar corrutinas

public class PlataformaCurva : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;
    public float alturaArco = 2f; // Altura máxima de la curva

    private float tiempo = 0f;
    private Gamepad gamepad; // Controlador detectado
    private Coroutine vibracionCoroutine; // Referencia a la corrutina

    void Start()
    {
        gamepad = Gamepad.current; // Obtiene el gamepad actual
    }

    void Update()
    {
        tiempo += Time.deltaTime * velocidad;
        float t = Mathf.PingPong(tiempo, 1f); // Movimiento de ida y vuelta

        // Interpolación lineal entre A y B
        Vector3 posicionLineal = Vector3.Lerp(puntoA.position, puntoB.position, t);

        // Agregar movimiento en forma de arco
        float desplazamientoY = Mathf.Sin(t * Mathf.PI) * alturaArco;
        posicionLineal.y -= desplazamientoY;

        transform.position = posicionLineal;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje")) 
        {
            other.transform.SetParent(transform); // Hace que el jugador se mueva con la plataforma
            if (vibracionCoroutine == null) // Evita iniciar varias veces la vibración
            {
                vibracionCoroutine = StartCoroutine(VibracionIntermitente());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            other.transform.SetParent(null); // Se desasocia al salir de la plataforma
            if (vibracionCoroutine != null)
            {
                StopCoroutine(vibracionCoroutine);
                vibracionCoroutine = null;
            }
            DetenerVibracion();
        }
    }

    IEnumerator VibracionIntermitente()
    {
        while (true) // Se repite mientras el personaje está sobre la plataforma
        {
            if (gamepad != null)
            {
                gamepad.SetMotorSpeeds(0.3f, 0.5f); // Activa la vibración
            }
            yield return new WaitForSeconds(0.2f); // Espera

            if (gamepad != null)
            {
                gamepad.SetMotorSpeeds(0f, 0f); // Desactiva la vibración
            }
            yield return new WaitForSeconds(0.3f); // Espera
        }
    }

    void DetenerVibracion()
    {
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0, 0); // Detiene la vibración
        }
    }
}
