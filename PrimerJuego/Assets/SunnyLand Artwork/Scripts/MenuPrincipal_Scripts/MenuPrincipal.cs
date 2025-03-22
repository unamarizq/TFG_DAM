using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar(){

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Salir(){
        Application.Quit();
    }
}
