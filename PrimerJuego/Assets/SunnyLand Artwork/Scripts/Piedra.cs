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
            SeguirJugadorArea.ReiniciarEscena();
        }
    }

}
