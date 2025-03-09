using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar(){

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
    }

    public void Salir(){
        Debug.Log("Salir...");
        Application.Quit();
    }
}
