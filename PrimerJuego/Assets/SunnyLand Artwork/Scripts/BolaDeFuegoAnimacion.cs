using UnityEngine;

public class BolaDeFuegoAnimacion : MonoBehaviour
{
    public Sprite[] sprites;          // Array de sprites para la animación
    public float tiempoEntreSprites = 0.1f; // Tiempo que pasa entre cada cambio de sprite

    private SpriteRenderer sr;
    private int indiceActual = 0;
    private float timer = 0f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tiempoEntreSprites)
        {
            timer = 0f;
            indiceActual++;

            if (indiceActual >= sprites.Length)
            {
                indiceActual = 0; // Volver al primer sprite
            }

            sr.sprite = sprites[indiceActual];
        }
    }
}

