using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{

    public float speed;
    public float dirX;
    public SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");

       // transform.position += new Vector3(dirX * speed, 0, 0);
        transform.Translate(Vector3.right * dirX * speed * Time.deltaTime);

    if(dirX>0){
        spr.flipX= false;
    }if(dirX<0){
        spr.flipX=true;
    }

    }
}
