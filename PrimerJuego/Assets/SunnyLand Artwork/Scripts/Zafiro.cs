using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zafiro : MonoBehaviour
{
    private Animator anim;
    private bool recolectada = false;
    private AudioSource audioSource;

    // Nuevo contador independiente para los zafiros
    public static int contadorZafiros = 3;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Obtiene el AudioSource
    }

    void OnTriggerEnter2D(Collider2D laCosa)
    {
        if (laCosa.CompareTag("Personaje") && !recolectada)
        {
            recolectada = true;

            contadorZafiros += 1;  // Suma uno al contador de Zafiros

            // Activa la animación de brillo
            anim.SetTrigger("Coger");

            audioSource.Play();

            // Desactiva el collider para que no se repita la colisión
            GetComponent<Collider2D>().enabled = false;

            // Espera a que termine la animación antes de destruir el zafiro
            StartCoroutine(DestruirDespuesDeAnimacion());
            // Espera a que termine el sonido y luego destruye el zafiro
            StartCoroutine(DestruirDespuesDeSonido());
        }
    }

    IEnumerator DestruirDespuesDeAnimacion()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    IEnumerator DestruirDespuesDeSonido()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}
