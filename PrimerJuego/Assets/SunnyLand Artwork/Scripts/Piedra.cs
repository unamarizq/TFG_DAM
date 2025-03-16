using UnityEngine;
using UnityEngine.SceneManagement;

public class Piedra : MonoBehaviour
{
    private bool enElSuelo = false; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enElSuelo = true;
        }

        if (collision.gameObject.CompareTag("Personaje") && !enElSuelo)
        {
            ReiniciarEscena();
        }
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
}
