using System.Collections;
using UnityEngine;

public class Gema : MonoBehaviour
{
    private Animator anim;
    private bool recolectada = false;
    private AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
         audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D laCosa)
    {
        if (laCosa.CompareTag("Personaje") && !recolectada)
        {
            recolectada = true;

            if(Punto_M.puntuajes >=99){
                Punto_M.puntuajes = 0;
                Zafiro.contadorZafiros+=1;
            }
            else{
                Punto_M.puntuajes += 1;
            }
            
            // Activa la animación de brillo
            anim.SetTrigger("Coger");

            audioSource.Play();

            // Desactiva el collider para que no se repita la colisión
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(DestruirDespuesDeAnimacion());
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

