using UnityEngine;
using UnityEngine.Audio;


public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void PantallaCompleta(bool pantallaComapleta)
    {
        Screen.fullScreen = pantallaComapleta;
    }

    public void CambiarVolumen(float volumen)
    {
        audioMixer.SetFloat("Volumen", volumen);
    }

    public void CambiarCalidad(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
