using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int nivelDestino;
    private bool jugadorDentro = false;

    void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
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
