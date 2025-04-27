using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CinematicaIntro : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videoFileName = "intro.mp4"; // nombre exacto del video
    public string siguienteEscena = "Lobby"; // nombre exacto de tu escena de juego

    void Start()
    {
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = videoPath;
        videoPlayer.Play();

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(siguienteEscena);
    }
}
