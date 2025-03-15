using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Punto_M : MonoBehaviour
{

    public Text puntuaje;
    public Text textoZafiros;

    public static int puntuajes;   
    // Start is called before the first frame update



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        puntuaje.text = puntuajes.ToString();
        textoZafiros.text = Zafiro.contadorZafiros.ToString();
    }
}
