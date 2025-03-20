using UnityEngine;

public class DetectorPinchos : MonoBehaviour
{
    public Palanca palanca; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Solo hace que los pinchos caigan si la palanca NO ha sido activada
        if (collision.CompareTag("Personaje") && !palanca.activado)
        {
            foreach (Transform pincho in palanca.pinchos)
            {
                if (pincho.GetComponent<Rigidbody2D>() == null) // Evitar múltiples Rigidbodies
                {
                    Rigidbody2D rb = pincho.gameObject.AddComponent<Rigidbody2D>();
                    rb.gravityScale = 2;
                }
            }
        }
    }
}
