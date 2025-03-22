using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{

    [SerializeField] private Vector2 velocidadMovimiento;

    private Vector2 offset;

    private Material material;

    private Rigidbody2D jugadorRB;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        jugadorRB = GameObject.FindGameObjectWithTag("Personaje").GetComponent<Rigidbody2D>();
        
    }
    
    void Update()
    {
        offset = jugadorRB.velocity.x * 0.1f * velocidadMovimiento * Time.deltaTime;
        material.mainTextureOffset += offset;

    }
}
