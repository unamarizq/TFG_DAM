using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.Events; // Importante para usar UnityEvent


public class Salto : MonoBehaviour
{

    public Rigidbody2D rbd;
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
        rbd.AddForce(Vector2.up * fuerza, ForceMode2D.Impulse);
        Suelo = false;
        anim.SetBool("Jump", true);
        //anim.SetBool("Fall", false); // Asegurarse de que no está cayendo aún
    }

    // Detectar cuando empieza a caer
    if (!Suelo) 
    {
        if (rbd.velocity.y > 0) // Sigue subiendo
        {
            anim.SetBool("Jump", true);
            //anim.SetBool("Fall", false);
        }
        else if (rbd.velocity.y < 0) // Empieza a caer
        {
            anim.SetBool("Jump", false);
            //anim.SetBool("Fall", true);
        }
    }
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
