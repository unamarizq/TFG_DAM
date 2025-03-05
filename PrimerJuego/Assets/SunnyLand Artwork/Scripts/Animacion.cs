using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animacion : MonoBehaviour
{

    public Animator anim;

    private bool Suelo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D)){
            
            anim.SetBool("Run",true);
        }

        if(Input.GetKeyUp(KeyCode.D)){
            anim.SetBool("Run",false);
        }

         if(Input.GetKeyDown(KeyCode.A)){
            anim.SetBool("Run",true);
        }

        if(Input.GetKeyUp(KeyCode.A)){
            anim.SetBool("Run",false);
        }
    }
}
