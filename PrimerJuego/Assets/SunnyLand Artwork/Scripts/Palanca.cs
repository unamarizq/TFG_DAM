using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Palanca : MonoBehaviour
{
    private bool dentroDelRango = false;
    public Transform[] pinchos; 
    public float desplazamiento = -2f;
    public float velocidadBajada = 2f;
    public bool activado = false;

    public Sprite imagenNormal; 
    public Sprite imagenActivada;
    private SpriteRenderer spriteRenderer;

    public bool peligroActivado = false; // Si los pinchos pueden caer

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Personaje"))
        {
            dentroDelRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Personaje"))
        {
            dentroDelRango = false;
        }
    }

    private void Update()
    {
        if ((dentroDelRango && Input.GetKeyDown(KeyCode.E) && !activado) || 
            (dentroDelRango && Gamepad.current?.buttonWest.wasPressedThisFrame == true && !activado))
        {
            StartCoroutine(BajarPinchos());
            activado = true;
            spriteRenderer.sprite = imagenActivada;
            HacerVibrarMando();
        }
        else if ((dentroDelRango && Input.GetKeyDown(KeyCode.X) && activado) ||
                 (dentroDelRango && Gamepad.current?.buttonEast.wasPressedThisFrame == true && activado))
        {
            activado = false;
            spriteRenderer.sprite = imagenNormal;
        }
    }

    private IEnumerator BajarPinchos()
    {
        Vector3[] posicionesIniciales = new Vector3[pinchos.Length];
        Vector3[] posicionesFinales = new Vector3[pinchos.Length];

        for (int i = 0; i < pinchos.Length; i++)
        {
            posicionesIniciales[i] = pinchos[i].position;
            posicionesFinales[i] = new Vector3(posicionesIniciales[i].x, posicionesIniciales[i].y + desplazamiento, posicionesIniciales[i].z);
        }

        float tiempo = 0f;
        while (tiempo < 1f)
        {
            for (int i = 0; i < pinchos.Length; i++)
            {
                pinchos[i].position = Vector3.Lerp(posicionesIniciales[i], posicionesFinales[i], tiempo);
            }
            tiempo += Time.deltaTime * velocidadBajada;
            yield return null;
        }

        for (int i = 0; i < pinchos.Length; i++)
        {
            pinchos[i].position = posicionesFinales[i];
        }

        peligroActivado = true; // Activa la caída si el jugador no desactiva la palanca
    }

    private void HacerVibrarMando()
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0.5f, 0.5f);
            Invoke("DetenerVibracion", 0.2f);
        }
    }

    private void DetenerVibracion()
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }
}
