using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Para compatibilidad con mando

public class Cofre : MonoBehaviour
{
    public Sprite cofreAbierto; // Asigna el sprite del cofre abierto desde el inspector
    private SpriteRenderer sr;
    private bool jugadorCerca = false;
    private bool abierto = false;

    public GameObject zafiroPrefab; // Prefab del zafiro
    public Transform puntoSpawn; // Lugar donde aparecerá el zafiro

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (jugadorCerca && !abierto && 
            (Input.GetKeyDown(KeyCode.E) || (Gamepad.current != null && Gamepad.current.buttonWest.wasPressedThisFrame)))
        {
            sr.sprite = cofreAbierto;
            abierto = true;

            if (zafiroPrefab != null && puntoSpawn != null)
            {
                Instantiate(zafiroPrefab, puntoSpawn.position, Quaternion.identity);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            jugadorCerca = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            jugadorCerca = false;
        }
    }
}
