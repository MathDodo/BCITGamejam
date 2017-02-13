#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateStateControllerWindow : EditorWindow
{
    //If you want the new scripts to be with comments
    private bool createWithComments = true;

    //Name of the allowed operator
    private string operatorName = "MachineOperator";

    //Name of the new controller
    private string controllerName = "NewStateController";

    //The path of the operators
    private string operatorPath = "Assets/StateFramework/MachineOperators/";

    //The path of the controllers
    private string controllerPath = "Assets/StateFramework/StateControllers/StateControllerCreators/";

    //Window to be shown
    private static EditorWindow window;

    /// <summary>
    /// Method for making the window
    /// </summary>
    public static void ShowWindow()
    {
        //Making the window
        window = GetWindow(typeof(CreateStateControllerWindow));

        //Setting the size of the window
        window.maxSize = new Vector2(300, 125);
        window.minSize = window.maxSize;
    }

    /// <summary>
    /// The ongui method called by unity 
    /// </summary>
    private void OnGUI()
    {
        //Making a space
        MakeSpace();

        //Enabling you to chose if you want the new scipt with comments
        createWithComments = EditorGUILayout.Toggle("Comments in new scripts", createWithComments);

        //Making spaces 
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Enabling you to change the name of the state controller and showing it in the window
        controllerName = EditorGUILayout.TextField("Name of new controller", controllerName);

        //Making spaces 
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Setting the allowed operator and showing it in the window
        operatorName = EditorGUILayout.TextField("Allowed operator", operatorName);

        //Making spaces 
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Making the create button
        if (GUILayout.Button("Create"))
        {
            //Checking if the operator extists
            if (File.Exists(operatorPath + operatorName + ".cs"))
            {
                if (!createWithComments)
                {
                    //Making the script
                    MakeScript();
                }
                else
                {
                    //Making the script
                    MakeScriptWithComments();
                }
            }
            else
            {
                //Showing the warning window with a message
                WarningWindow.ShowWindow("No operator of name " + operatorName + " exists");
            }
        }
    }

    /// <summary>
    /// Method for making spaces
    /// </summary>
    private void MakeSpace()
    {
        EditorGUILayout.Space();
    }

    /// <summary>
    /// Method for making a script
    /// </summary>
    private void MakeScript()
    {
        //Setting up the path for the new file
        string copyPath = controllerPath + controllerName + ".cs";

        //Checking if the file already exists
        if (!File.Exists(copyPath))
        {
            //Logging to console
            Debug.Log("Creating Classfile: " + copyPath);

            //Using a streamwriter to make the new controller file
            using (StreamWriter outfile = new StreamWriter(copyPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("");
                outfile.WriteLine("[CreateAssetMenu(fileName = \"" + controllerName + "\", menuName = \"StateControllers/" + controllerName + "\", order = 2)]");
                outfile.WriteLine("public class " + controllerName + " : StateControllerGeneric<" + operatorName + ">");
                outfile.WriteLine("{");
                outfile.WriteLine("}");
            }

            //Setting up the path for the custom inspector for the controller
            string inspectorPath = "Assets/StateFramework/EditorExtensions/StateControllerInspectors/" + controllerName + "Inspector.cs";

            //Using a streamwriter to make the new file
            using (StreamWriter outfile = new StreamWriter(inspectorPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using UnityEditor;");
                outfile.WriteLine("");
                outfile.WriteLine("[CustomEditor(typeof(" + controllerName + "))]");
                outfile.WriteLine("public class " + controllerName + "Inspector : Editor");
                outfile.WriteLine("{");
                outfile.WriteLine("    private " + controllerName + " stateController;");
                outfile.WriteLine("");
                outfile.WriteLine("    private void OnEnable()");
                outfile.WriteLine("    {");
                outfile.WriteLine("         stateController = (" + controllerName + ")target;");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    public override void OnInspectorGUI()");
                outfile.WriteLine("    {");
                outfile.WriteLine("         GUILayout.Label(\"States in controller: \");");
                outfile.WriteLine("         for (int i = 0; i < stateController.AllowedStates.Count; i++)");
                outfile.WriteLine("         {");
                outfile.WriteLine("            if (stateController.AllowedStates[i].StateName == string.Empty)");
                outfile.WriteLine("            {");
                outfile.WriteLine("                stateController.AllowedStates[i].SetStateName();");
                outfile.WriteLine("            }");
                outfile.WriteLine("            GUILayout.Label(stateController.AllowedStates[i].StateName);");
                outfile.WriteLine("         }");
                outfile.WriteLine("");
                outfile.WriteLine("         if (GUILayout.Button(\"Add state\"))");
                outfile.WriteLine("         {");
                outfile.WriteLine("             AddStateWindow.ShowWindow(stateController);");
                outfile.WriteLine("         }");
                outfile.WriteLine("");
                outfile.WriteLine("         if (GUILayout.Button(\"Remove state\"))");
                outfile.WriteLine("         {");
                outfile.WriteLine("              RemoveStateWindow.ShowWindow(stateController);");
                outfile.WriteLine("         }");
                outfile.WriteLine("");
                outfile.WriteLine("         if (GUILayout.Button(\"Clear states\"))");
                outfile.WriteLine("         {");
                outfile.WriteLine("              stateController.ClearStates();");
                outfile.WriteLine("         }");
                outfile.WriteLine("    }");
                outfile.WriteLine("}");
            }

            //Updating the asset database
            AssetDatabase.Refresh();

            //Closing the window
            window.Close();
        }
        else
        {
            //Showing the warning window with a message
            WarningWindow.ShowWindow("The controller name " + controllerName + " is already in use");
        }
    }

    private void MakeScriptWithComments()
    {
        //Changing the path
        string copyPath = controllerPath + controllerName + ".cs";

        //Checking if the file already exists
        if (!File.Exists(copyPath))
        {
            //Logging to console
            Debug.Log("Creating Classfile: " + copyPath);

            //Using a streamwriter to make the new file
            using (StreamWriter outfile = new StreamWriter(copyPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("");
                outfile.WriteLine("/// <summary>");
                outfile.WriteLine("/// The specified controller for the demo operator, made so you will have a creating option in the inspector, having a controller for the specified machine,");
                outfile.WriteLine("/// and for having a specified inspector window.");
                outfile.WriteLine("/// </summary>");
                outfile.WriteLine("[CreateAssetMenu(fileName = \"" + controllerName + "\", menuName = \"StateControllers/" + controllerName + "\", order = 2)]");
                outfile.WriteLine("public class " + controllerName + " : StateControllerGeneric<" + operatorName + ">");
                outfile.WriteLine("{");
                outfile.WriteLine("}");
            }

            //Setting up the path for the custom inspector for the controller
            string inspectorPath = "Assets/StateFramework/EditorExtensions/StateControllerInspectors/" + controllerName + "Inspector.cs";

            //Using a streamwriter to make the new file
            using (StreamWriter outfile = new StreamWriter(inspectorPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using UnityEditor;");
                outfile.WriteLine("");
                outfile.WriteLine("/// <summary>");
                outfile.WriteLine("/// This is the special inspector for the controller which is specified by the custom editor");
                outfile.WriteLine("/// </summary>");
                outfile.WriteLine("[CustomEditor(typeof(" + controllerName + "))]");
                outfile.WriteLine("public class " + controllerName + "Inspector : Editor");
                outfile.WriteLine("{");
                outfile.WriteLine("    //The controller for the inspector");
                outfile.WriteLine("    private " + controllerName + " stateController;");
                outfile.WriteLine("");
                outfile.WriteLine("    /// <summary>");
                outfile.WriteLine("    /// When the inspector enables this will set the stateController to be the target of the inspector");
                outfile.WriteLine("    /// </summary>");
                outfile.WriteLine("    private void OnEnable()");
                outfile.WriteLine("    {");
                outfile.WriteLine("         stateController = (" + controllerName + ")target;");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    /// <summary>");
                outfile.WriteLine("    /// This method will be called each time the inspector render something");
                outfile.WriteLine("    /// <summary>");
                outfile.WriteLine("    public override void OnInspectorGUI()");
                outfile.WriteLine("    {");
                outfile.WriteLine("         //Setting the label for the custom inspector");
                outfile.WriteLine("         GUILayout.Label(\"States in controller: \");");
                outfile.WriteLine("");
                outfile.WriteLine("         //Writing out the allowed states in the controller");
                outfile.WriteLine("         for (int i = 0; i < stateController.AllowedStates.Count; i++)");
                outfile.WriteLine("         {");
                outfile.WriteLine("            //Making sure to set the names of the different states");
                outfile.WriteLine("            if (stateController.AllowedStates[i].StateName == string.Empty)");
                outfile.WriteLine("            {");
                outfile.WriteLine("                stateController.AllowedStates[i].SetStateName();");
                outfile.WriteLine("            }");
                outfile.WriteLine("            //Setting up the label with the state name");
                outfile.WriteLine("            GUILayout.Label(stateController.AllowedStates[i].StateName);");
                outfile.WriteLine("         }");
                outfile.WriteLine("");
                outfile.WriteLine("         //Making the button to add states");
                outfile.WriteLine("         if (GUILayout.Button(\"Add state\"))");
                outfile.WriteLine("         {");
                outfile.WriteLine("             //Opening another window which will let states be added to the controller if the button is clicked");
                outfile.WriteLine("             AddStateWindow.ShowWindow(stateController);");
                outfile.WriteLine("         }");
                outfile.WriteLine("");
                outfile.WriteLine("         //Making the button to remove states");
                outfile.WriteLine("         if (GUILayout.Button(\"Remove state\"))");
                outfile.WriteLine("         {");
                outfile.WriteLine("              //Opening a window which wil let states be removed by their names if the button is clicked");
                outfile.WriteLine("              RemoveStateWindow.ShowWindow(stateController);");
                outfile.WriteLine("         }");
                outfile.WriteLine("");
                outfile.WriteLine("         //Making a button to clear the list of allowed states");
                outfile.WriteLine("         if (GUILayout.Button(\"Clear states\"))");
                outfile.WriteLine("         {");
                outfile.WriteLine("              //Clearing the states if the button is clicked");
                outfile.WriteLine("              stateController.ClearStates();");
                outfile.WriteLine("         }");
                outfile.WriteLine("    }");
                outfile.WriteLine("}");
            }

            //Updating the asset database
            AssetDatabase.Refresh();

            //Closing the window
            window.Close();
        }
        else
        {
            //Showing the warning window with a message
            WarningWindow.ShowWindow("The controller name " + controllerName + " is already in use");
        }
    }
}
#endif
