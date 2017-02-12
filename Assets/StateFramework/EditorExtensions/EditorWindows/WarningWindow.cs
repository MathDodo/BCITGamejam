#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class WarningWindow : EditorWindow
{
    //The message to be shown as a warning
    private static string message;

    //The window to be shown
    private static EditorWindow window;

    public static void ShowWindow(string msg)
    {
        //Setting the message
        message = msg;

        //Making the window
        window = GetWindow(typeof(WarningWindow));

        //Setting the size of the window
        window.maxSize = new Vector2(500, 70);
        window.minSize = window.maxSize;
    }

    /// <summary>
    /// The ongui method called by unity 
    /// </summary>
    private void OnGUI()
    {
        //Showing the message in the window
        EditorGUILayout.TextField(message);

        //Making four spaces
        for (int i = 0; i < 4; i++)
        {
            MakeSpace();
        }

        //Setting up the close button in the middle of the window
        GUILayout.BeginArea(new Rect((Screen.width / 2) - 50, 50, 100, 100));
        if (GUILayout.Button("Close"))
        {
            //Closing the window if the button is clicked
            window.Close();
        }
        GUILayout.EndArea();
    }

    /// <summary>
    /// Method for making spaces
    /// </summary>
    private void MakeSpace()
    {
        EditorGUILayout.Space();
    }
}
#endif
