using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{

    public float speed;
    public float dirX;
    public SpriteRenderer spr;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");

        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // Usar deltaTime
        rb.velocity = new Vector2(moveX / Time.deltaTime, rb.velocity.y);

        if (dirX>0){
        spr.flipX= false;
    }if(dirX<0){
        spr.flipX=true;
    }

    }

    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Pinchos"))
    {
        Debug.Log("El personaje tocó los pinchos. Reiniciando...");
        ReiniciarEscena();
    }
}

private void ReiniciarEscena()
{
    // Resta un zafiro
    Zafiro.contadorZafiros--;

    if (Zafiro.contadorZafiros <= 0)
    {
        // Si no quedan zafiros, ir a la pantalla de inicio
        SceneManager.LoadScene("MenuPrincipal");
        Zafiro.contadorZafiros = 3;
    }
    else
    {
        // Si aún quedan zafiros, reiniciar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
}
