using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaNivel : MonoBehaviour
{
    public AudioSource audioSource;
    private bool hasPlayed = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasPlayed) return;  // Evita que el sonido se reproduzca más de una vez
        audioSource.Play();
        hasPlayed = true;  // Marca que el sonido ya se ha reproducido
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
