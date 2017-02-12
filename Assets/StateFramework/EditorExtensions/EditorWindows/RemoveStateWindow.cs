#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class RemoveStateWindow : EditorWindow
{
    //Name of the state to remove you can see the names of states in the custon editor
    private string stateToRemove = "State";

    //The window to be shown
    private static EditorWindow window;

    //The controller to remove a state from
    private static StateController controller;

    /// <summary>
    /// Method to show the window
    /// </summary>
    /// <param name="targetController"></param>
    public static void ShowWindow(StateController targetController)
    {
        //Setting the controller which will have a state removed
        controller = targetController;

        //Making the window
        window = GetWindow(typeof(RemoveStateWindow));

        //Setting the size of the window
        window.maxSize = new Vector2(250, 75);
        window.minSize = window.maxSize;
    }

    /// <summary>
    /// The ongui method called by unity 
    /// </summary>
    private void OnGUI()
    {
        //The name of the state to be removed, shown in the window where you can change it
        stateToRemove = EditorGUILayout.TextField("Name of state", stateToRemove);

        //Making four spaces
        for (int i = 0; i < 4; i++)
        {
            MakeSpace();
        }

        //Setting up the button called remove state in the middle of the window
        GUILayout.BeginArea(new Rect((Screen.width / 2) - 50, 55, 100, 100));
        if (GUILayout.Button("Remove state", GUILayout.Width(100)) && controller.AllowedStates.Any(c => c.StateName == stateToRemove))
        {
            //Removing the state from the controller if there is a state with the name
            controller.RemoveState(stateToRemove);

            //Closing the window
            window.Close();
        }
        GUILayout.EndArea();
    }

    /// <summary>
    /// Method for making spaces in the window
    /// </summary>
    private void MakeSpace()
    {
        EditorGUILayout.Space();
    }
}
#endif
