using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gema : MonoBehaviour
{
    private Animator anim;
    private bool recolectada = false;
    private AudioSource audioSource;

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

            // Aumenta la puntuación
            Punto_M.puntuajes += 1;

            // Activa la animación de brillo
            anim.SetTrigger("Coger");

            // Reproduce el sonido
            audioSource.Play();

            // Desactiva el collider para que no se repita la colisión
            GetComponent<Collider2D>().enabled = false;

            // Espera a que termine la animación antes de destruir la gema
            StartCoroutine(DestruirDespuesDeAnimacion());
            // Espera a que termine el sonido y luego destruye la gema
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

