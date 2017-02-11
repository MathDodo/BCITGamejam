using UnityEditor;
using UnityEngine;

public class AddStateWindow : EditorWindow
{
    //The state you want to add to the controller
    private State stateToAdd;

    //The window to be shown
    private static EditorWindow window;

    //The controller you to add something to
    private static StateController controller;

    /// <summary>
    /// Method to show the window and get the controller
    /// </summary>
    /// <param name="targetController"></param>
    public static void ShowWindow(StateController targetController)
    {
        //Setting the controller you want to add a state to
        controller = targetController;

        //Making the window
        window = GetWindow(typeof(AddStateWindow));

        //Setting the window size
        window.maxSize = new Vector2(350, 75);
        window.minSize = window.maxSize;
    }

    /// <summary>
    /// The unity called ongui method
    /// </summary>
    private void OnGUI()
    {
        //Setting the state by drag and drop or by finding it in the assets by clicking on the circle
        stateToAdd = (State)EditorGUILayout.ObjectField(new GUIContent("State to add"), stateToAdd, typeof(State), false);

        //Checking if the state is null
        if (stateToAdd != null)
        {
            //Setting the states operator type
            stateToAdd.SetOperatorType();

            //Setting the state type
            stateToAdd.SetStateType();

            //Setting the controller's operator type
            controller.SetControllerOperatorType();

            //Checking if the state and the controller allows the same operator
            if (stateToAdd.OperatorType != controller.ControllerType)
            {
                //Setting state to null if they have different operators
                stateToAdd = null;

                //Showing the warning window with a message
                WarningWindow.ShowWindow("This state is of the wrong type!");
            }
            //Checking if the controller already contains the statetype cant have copies of the same state
            else if (controller.AllowedStates.Any(s => s.StateType == stateToAdd.StateType))
            {
                //Setting state to null if the controller already contains the state type
                stateToAdd = null;

                //Showing the warning window with a message
                WarningWindow.ShowWindow("This state type is already in the controller!");
            }
            //Adding the state if nothing else is wrong
            else
            {
                //Making five spaces
                for (int i = 0; i < 5; i++)
                {
                    MakeSpace();
                }

                //Making the add state button in the middle of the window
                GUILayout.BeginArea(new Rect((Screen.width / 2) - 50, 55, 100, 100));
                if (GUILayout.Button("Add state", GUILayout.Width(100)))
                {
                    //Setting the name of the new state to show it in the custom controller inspector
                    stateToAdd.SetStateName();

                    //Adding the state
                    controller.AddState(stateToAdd);

                    //Closing the window
                    window.Close();
                }
                GUILayout.EndArea();
            }
        }
    }

    /// <summary>
    /// Method to make spaces
    /// </summary>
    private void MakeSpace()
    {
        EditorGUILayout.Space();
    }
}
