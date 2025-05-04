using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransicionMuerte : MonoBehaviour
{
    public Animator anim;

    public void IniciarTransicion()
    {
        anim.SetTrigger("FadeOut");
        StartCoroutine(RE());
    }

    IEnumerator RE()
    {
        yield return new WaitForSeconds(1.5f); // Espera que acabe el fade
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
