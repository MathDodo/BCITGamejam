using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("New level 1");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MainMenu");
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
