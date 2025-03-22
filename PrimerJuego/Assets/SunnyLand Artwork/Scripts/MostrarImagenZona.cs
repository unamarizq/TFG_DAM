using UnityEngine;
using UnityEngine.UI;

public class MostrarImagenZona : MonoBehaviour
{
    public GameObject imagen; // Arrastra la imagen en el Inspector

    private void Start()
    {
        imagen.SetActive(false); // Oculta la imagen al inicio
        Debug.Log("Script iniciado, imagen oculta.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Objeto detectado: " + other.gameObject.name);
        
        if (other.CompareTag("Personaje"))
        {
            Debug.Log("Personaje detectado, mostrando imagen.");
            imagen.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            Debug.Log("Personaje salió de la zona, ocultando imagen.");
            imagen.SetActive(false);
        }
    }
}
