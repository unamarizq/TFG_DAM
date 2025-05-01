using UnityEngine;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{

    public float speed;
    public float dirX;
    public SpriteRenderer spr;
    Rigidbody2D rb;
    private bool velocidadAumentada = false;

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
        ReiniciarEscena();
    }
}

private void ReiniciarEscena()
{
    Zafiro.contadorZafiros--;

    if (Zafiro.contadorZafiros <= 0)
    {
        SceneManager.LoadScene("GameOverVideo");
        Zafiro.contadorZafiros = 3;
    }
    else
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

public void ComprobarVelocidad()
{
    if (Cristales.contadorCristales == 5 && !velocidadAumentada)
    {
        speed += 2.5f;
        velocidadAumentada = true;
        
    }
}

}
