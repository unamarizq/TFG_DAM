using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Punto_M : MonoBehaviour
{
    public Text puntuaje;
    public Text textoZafiros;
    public Text textoCristales;
    public GameObject imagenSpeed;
    public GameObject imagenJump;

    public static int puntuajes;

    private bool imagenJumpMostrada = false;  // Controla si la imagenJump ya se ha mostrado

    void Start()
    {
        imagenSpeed.SetActive(false);
        imagenJump.SetActive(false);
    }

    void Update()
    {
        puntuaje.text = puntuajes.ToString();
        textoZafiros.text = Zafiro.contadorZafiros.ToString();
        textoCristales.text = Cristales.contadorCristales.ToString();

        // Mostrar la imagenSpeed cuando se alcanzan 5 cristales
        if (Cristales.contadorCristales == 5)
        {
            imagenSpeed.SetActive(true);  // Mostrar la imagen de velocidad
        }
        else if (Cristales.contadorCristales < 5)
        {
            imagenSpeed.SetActive(false); // Ocultar imagenSpeed si el contador es menor a 5
        }

        // Mostrar la imagenJump cuando se alcanzan 10 cristales, pero solo una vez
        if (Cristales.contadorCristales >= 10 && !imagenJumpMostrada)
        {
            imagenJumpMostrada = true;  // Marcar que la imagen ya se ha mostrado
            imagenJump.SetActive(true); // Mostrar la imagen de salto
            StartCoroutine(OcultarImagen());  // Comenzar corutina para ocultar la imagen después de 10 segundos
        }

        // Resetea el contador de cristales después de que la imagenJump se haya mostrado
        if (Cristales.contadorCristales >= 10 && imagenJumpMostrada)
        {
            // Este código es opcional, solo si deseas resetear el contador
            Cristales.contadorCristales = 5;
        }
    }

    // Corutina para ocultar la imagenJump después de 10 segundos
    IEnumerator OcultarImagen()
    {
        yield return new WaitForSeconds(10);  // Espera 10 segundos
        imagenJump.SetActive(false);         // Oculta la imagen de salto
        imagenJumpMostrada = false;          // Permite que la imagen se muestre nuevamente si se alcanza 10 cristales
    }
}
