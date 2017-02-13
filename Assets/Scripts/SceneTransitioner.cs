using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("");
    }

    public void Credits()
    {
        SceneManager.LoadScene("");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
