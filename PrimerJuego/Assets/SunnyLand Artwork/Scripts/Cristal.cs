using System.Collections;
using UnityEngine;

public class Cristales : MonoBehaviour
{
    private Animator anim;
    private bool recolectado = false;
    private AudioSource audioSource;

    public static int contadorCristales = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D laCosa)
    {
        if (laCosa.CompareTag("Personaje") && !recolectado)
        {
            recolectado = true;
            contadorCristales++;

            anim.SetTrigger("Coger");
            audioSource.Play();

            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(DestruirDespuesDeAnimacion());
            StartCoroutine(DestruirDespuesDeSonido());

            // Aplicar efectos si el personaje ha recolectado suficiente
            laCosa.GetComponent<Movimiento>()?.ComprobarVelocidad();
            laCosa.GetComponent<Salto>()?.ComprobarSalto();
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
