using UnityEngine;
using System.Collections;

public class MostrarImagenZona : MonoBehaviour
{
    public GameObject imagen; 
    private SpriteRenderer spriteRenderer;
    private Coroutine fadeCoroutine;

    private void Start()
    {
        spriteRenderer = imagen.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró SpriteRenderer en el objeto de imagen.");
            return;
        }

        Color color = spriteRenderer.color;
        color.a = 0; // Hacer la imagen invisible al inicio
        spriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        while (spriteRenderer.color.a < 1)
        {
            Color color = spriteRenderer.color;
            color.a += Time.deltaTime * 2; // Ajusta la velocidad
            spriteRenderer.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while (spriteRenderer.color.a > 0)
        {
            Color color = spriteRenderer.color;
            color.a -= Time.deltaTime * 2;
            spriteRenderer.color = color;
            yield return null;
        }
    }
}
