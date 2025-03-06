using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.Events; // Importante para usar UnityEvent


public class Salto : MonoBehaviour
{

    public Rigidbody2D rb;
    public float fuerza;

    public Animator anim;

    private bool Suelo;

    // Evento para detectar cuando toca el suelo
    public UnityEvent OnLandEvent;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
{
    if ((Input.GetKeyDown(KeyCode.Space) || (Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame)) && Suelo == true)
    {
        float moveY = fuerza * Time.deltaTime; // Usar deltaTime
        rb.velocity = new Vector2(rb.velocity.x, moveY / Time.deltaTime);
        Suelo = false;
        anim.SetBool("Jump", true);
    }

        anim.SetBool("OnGround", Suelo);
        anim.SetFloat("YVelocity", rb.velocity.y);
}


    public void OnLanding(){
        anim.SetBool("Jump", false);
    }

    public void OnCollisionEnter2D(Collision2D laCosa){

        Debug.Log("Toque algo");

        if(laCosa.gameObject.tag == "Suelo"){
            Debug.Log("Toque suelo");
            anim.SetBool("Jump",false);
            Suelo=true;

            // Llamamos al evento cuando toca el suelo
            if (OnLandEvent != null)
                OnLandEvent.Invoke();

        }
        

    }

    
}
