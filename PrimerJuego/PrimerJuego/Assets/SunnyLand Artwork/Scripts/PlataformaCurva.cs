using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;

public class PlataformaCurva : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;
    public float alturaArco = 2f;

    private float tiempo = 0f;
    private Gamepad gamepad;
    private Coroutine vibracionCoroutine;

    void Start()
    {
        gamepad = Gamepad.current;
    }

    void Update()
    {
        tiempo += Time.deltaTime * velocidad;
        float t = Mathf.PingPong(tiempo, 1f); // Movimiento de ida y vuelta

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
        while (true) 
        {
            if (gamepad != null)
            {
                gamepad.SetMotorSpeeds(0.3f, 0.5f);
            }
            yield return new WaitForSeconds(0.2f);

            if (gamepad != null)
            {
                gamepad.SetMotorSpeeds(0f, 0f);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void DetenerVibracion()
    {
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(0, 0);
        }
    }
}
