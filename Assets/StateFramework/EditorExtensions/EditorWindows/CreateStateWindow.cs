#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateStateWindow : EditorWindow
{
    //If you want the new scripts to be with comments
    private bool createWithComments = true;

    //The name of the new state
    private string stateName = "StateName";

    //The allowed operator
    private string operatorName = "MachineOperator";

    //The path of the operators
    private string operatorPath = "Assets/StateFramework/MachineOperators/";

    //The path of the states
    private string statePath = "Assets/StateFramework/States/StateCreators/";

    //The window to be shown
    private static EditorWindow window;

    /// <summary>
    /// Method for setting and showing the window
    /// </summary>
    public static void ShowWindow()
    {
        //Making the window
        window = GetWindow(typeof(CreateStateWindow));

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

        //Enabling you to change the name of the state and showing it in the window
        stateName = EditorGUILayout.TextField("The state name", stateName);

        //Making spaces
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Enabling you to change the name of the allowed operator and showing it in the window
        operatorName = EditorGUILayout.TextField("Allowed operator", operatorName);

        //Making  spaces
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Making the create button
        if (GUILayout.Button("Create"))
        {
            //Checking if the allowed operator exists
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
                WarningWindow.ShowWindow("No operator with the name " + operatorName + " exists");
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
    /// Method for making the script
    /// </summary>
    private void MakeScript()
    {
        //Setting the path for the file
        string copyPath = statePath + stateName + ".cs";

        if (File.Exists(copyPath) == false)
        {
            //Logging to console
            Debug.Log("Creating Classfile: " + copyPath);

            //Using a streamwriter to make the new file
            using (StreamWriter outfile = new StreamWriter(copyPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using System.Collections.Generic;");
                outfile.WriteLine("");
                outfile.WriteLine("[CreateAssetMenu(fileName = \"" + stateName + "\", menuName = \"States/" + stateName + "\", order = 1)]");
                outfile.WriteLine("public class " + stateName + " : StateGeneric<" + operatorName + ">");
                outfile.WriteLine("{");
                outfile.WriteLine("    [SerializeField]");
                outfile.WriteLine("    private string stateName = \"" + stateName + "\";");
                outfile.WriteLine("");
                outfile.WriteLine("    public override void Enter(" + operatorName + " user)");
                outfile.WriteLine("    {");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    public override void Execute(" + operatorName + " user)");
                outfile.WriteLine("    {");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    public override void Exit(" + operatorName + " user)");
                outfile.WriteLine("    {");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    public override bool IsReadyToExit(" + operatorName + " user)");
                outfile.WriteLine("    {");
                outfile.WriteLine("        return true;");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    public override void SetStateType()");
                outfile.WriteLine("    {");
                outfile.WriteLine("        StateType = typeof(" + stateName + ");");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    public override void SetStateName()");
                outfile.WriteLine("    {");
                outfile.WriteLine("        StateName = stateName;");
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
            //Showing warning window with a message
            WarningWindow.ShowWindow("The state called " + stateName + " already exists");
        }
    }

    private void MakeScriptWithComments()
    {
        //Changing the path
        string copyPath = statePath + stateName + ".cs";

        if (File.Exists(copyPath) == false)
        {
            //Logging to console
            Debug.Log("Creating Classfile: " + copyPath);

            //Using a streamwriter to make the new file
            using (StreamWriter outfile = new StreamWriter(copyPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using System.Collections.Generic;");
                outfile.WriteLine("");
                outfile.WriteLine("/// <summary>");
                outfile.WriteLine("/// This is a class for making specified functionality for the state,");
                outfile.WriteLine("/// and to make the creation of the scriptable object possible.");
                outfile.WriteLine("/// </summary>");
                outfile.WriteLine("[CreateAssetMenu(fileName = \"" + stateName + "\", menuName = \"States/" + stateName + "\", order = 1)]");
                outfile.WriteLine("public class " + stateName + " : StateGeneric<" + operatorName + ">");
                outfile.WriteLine("{");
                outfile.WriteLine("    [SerializeField]");
                outfile.WriteLine("    //The name of the state also exposed for the editor");
                outfile.WriteLine("    private string stateName = \"" + stateName + "\";");
                outfile.WriteLine("");
                outfile.WriteLine("    /// <summary>");
                outfile.WriteLine("    /// Method which is called when a user enters this state, normally when the user changes states");
                outfile.WriteLine("    /// </summary>");
                outfile.WriteLine("    /// <param name=\"user\"></param>");
                outfile.WriteLine("    public override void Enter(" + operatorName + " user)");
                outfile.WriteLine("    {");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    /// <summary>");
                outfile.WriteLine("    /// Mehtod which is called when the user wants to execute, probably an execute is called each frame");
                outfile.WriteLine("    /// </summary>");
                outfile.WriteLine("    /// <param name=\"user\"></param>");
                outfile.WriteLine("    public override void Execute(" + operatorName + " user)");
                outfile.WriteLine("    {");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    /// <summary>");
                outfile.WriteLine("    /// Method which is called when a user exists this state, normally when the user changes states");
                outfile.WriteLine("    /// </summary>");
                outfile.WriteLine("    /// <param name=\"user\"></param>");
                outfile.WriteLine("    public override void Exit(" + operatorName + " user)");
                outfile.WriteLine("    {");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    /// <summary>");
                outfile.WriteLine("    /// This method is run to check if this state is ready to be exited, if you want a user to be in a state for any amount time this is where you stop it from exiting");
                outfile.WriteLine("    /// </summary>");
                outfile.WriteLine("    /// <param name=\"user\"></param>");
                outfile.WriteLine("    public override bool IsReadyToExit(" + operatorName + " user)");
                outfile.WriteLine("    {");
                outfile.WriteLine("        return true;");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    /// <summary>");
                outfile.WriteLine("    /// Setting the state type");
                outfile.WriteLine("    /// </summary>");
                outfile.WriteLine("    public override void SetStateType()");
                outfile.WriteLine("    {");
                outfile.WriteLine("        StateType = typeof(" + stateName + ");");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    /// <summary>");
                outfile.WriteLine("    /// Setting the state name");
                outfile.WriteLine("    /// </summary>");
                outfile.WriteLine("    public override void SetStateName()");
                outfile.WriteLine("    {");
                outfile.WriteLine("        StateName = stateName;");
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
            //Showing warning window with a message
            WarningWindow.ShowWindow("The state called " + stateName + " already exists");
        }
    }
}
#endif
