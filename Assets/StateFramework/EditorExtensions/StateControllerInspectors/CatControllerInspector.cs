using UnityEditor;
using UnityEngine;

/// <summary>
/// This is the special inspector for the controller which is specified by the custom editor
/// </summary>
[CustomEditor(typeof(CatController))]
public class CatControllerInspector : Editor
{
    //The controller for the inspector
    private CatController stateController;

    /// <summary>
    /// When the inspector enables this will set the stateController to be the target of the inspector
    /// </summary>
    private void OnEnable()
    {
        stateController = (CatController)target;
    }

    /// <summary>
    /// This method will be called each time the inspector render something
    /// <summary>
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //Setting the label for the custom inspector
        GUILayout.Label("States in controller: ");

        //Writing out the allowed states in the controller
        for (int i = 0; i < stateController.AllowedStates.Count; i++)
        {
            //Making sure to set the names of the different states
            if (stateController.AllowedStates[i].StateName == string.Empty)
            {
                stateController.AllowedStates[i].SetStateName();
            }
            //Setting up the label with the state name
            GUILayout.Label(stateController.AllowedStates[i].StateName);
        }

        //Making the button to add states
        if (GUILayout.Button("Add state"))
        {
            //Opening another window which will let states be added to the controller if the button is clicked
            AddStateWindow.ShowWindow(stateController);
        }

        //Making the button to remove states
        if (GUILayout.Button("Remove state"))
        {
            //Opening a window which wil let states be removed by their names if the button is clicked
            RemoveStateWindow.ShowWindow(stateController);
        }

        //Making a button to clear the list of allowed states
        if (GUILayout.Button("Clear states"))
        {
            //Clearing the states if the button is clicked
            stateController.ClearStates();
        }
    }
}
