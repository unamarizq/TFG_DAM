using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Portal : MonoBehaviour
{
    public int nivelDestino;
    private bool jugadorDentro = false;

    void Update()
    {
        if (jugadorDentro && (Input.GetKeyDown(KeyCode.E) || 
        (Gamepad.current != null && Gamepad.current.buttonWest.wasPressedThisFrame)))
        {
            Cristales.contadorCristales = 0;
            SceneManager.LoadScene(nivelDestino);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            jugadorDentro = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            jugadorDentro = false;
        }
    }
}
