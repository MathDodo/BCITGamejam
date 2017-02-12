using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField]
    private CanvasRenderer menu;

    [SerializeField]
    private CanvasRenderer credits;

    public void Play()
    {
        SceneManager.LoadScene("");
    }

    public void Credits()
    {
        menu.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
    }

    public void Menu()
    {
        menu.gameObject.SetActive(true);
        credits.gameObject.SetActive(false);
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
