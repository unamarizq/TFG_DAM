using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DetectorPiedras : MonoBehaviour
{
    public GameObject[] piedras;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Personaje"))
        {
            StartCoroutine(CaerPiedrasConRetraso());
            StartCoroutine(VibrarMando());
        }
    }

    private IEnumerator CaerPiedrasConRetraso()
    {
        yield return new WaitForSeconds(1.0f);

        foreach (GameObject piedra in piedras)
        {
            // Si la piedra no tiene Rigidbody2D, se lo añadimos para que caiga
            if (piedra.GetComponent<Rigidbody2D>() == null)
            {
                Rigidbody2D rb = piedra.AddComponent<Rigidbody2D>();
                rb.gravityScale = 2;
            }
        }
    }

     private IEnumerator VibrarMando()
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0.5f, 0.5f); 

            yield return new WaitForSeconds(2.0f);

            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }
}
