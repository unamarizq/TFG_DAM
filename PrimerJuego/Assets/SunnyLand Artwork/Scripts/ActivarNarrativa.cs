using UnityEngine;

public class ActivarNarrativa : MonoBehaviour
{
    public AudioSource audioNarrativa;
    private bool yaSeReprodujo = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!yaSeReprodujo && other.CompareTag("Personaje"))
        {
            audioNarrativa.Play();
            yaSeReprodujo = true;
        }
    }
}
