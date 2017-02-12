using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateOperatorWindow : EditorWindow
{
    //If you want the new scripts to be with comments
    private bool createWithComments = true;

    //The mark of the target machine 
    private string machineName = "TargetMachineMark";

    //The name of the new operator file
    private string operatorName = "NewStateMachineHandler";

    //The path of the operator file
    private string operatorPath = "Assets/StateFramework/MachineOperators/";

    //The window which will be shown
    private static EditorWindow window;

    /// <summary>
    /// Static method to show the window
    /// </summary>
    public static void ShowWindow()
    {
        //Making the window 
        window = GetWindow(typeof(CreateOperatorWindow));

        //Setting the size of the window
        window.maxSize = new Vector2(300, 125);
        window.minSize = window.maxSize;
    }

    /// <summary>
    /// The ongui method called by unity 
    /// </summary>
    private void OnGUI()
    {
        //Starting by making a space in the window
        MakeSpace();

        //Enabling you to chose if you want the new scipt with comments
        createWithComments = EditorGUILayout.Toggle("Comments in new scripts", createWithComments);

        //Making spaces
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Enabling you to change the name of the operator and showing it in the window
        operatorName = EditorGUILayout.TextField("Operator name", operatorName);

        //Making spaces
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Enabling you to change the mark of the state machine and showing it in the window
        machineName = EditorGUILayout.TextField("Target machine mark", machineName);

        //Making spaces
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Making the create button
        if (GUILayout.Button("Create"))
        {
            //Checking if there is a mark which you specified if the create button was clicked
            if (Enum.IsDefined(typeof(MachineMarker), machineName))
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
                //Showing a warning if the machine mark doesnt exist
                WarningWindow.ShowWindow("There is no target machine by the name " + machineName);
            }
        }

    }

    /// <summary>
    /// Method for making spaces in the window
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
        //Setting up the path for the new file
        string copyPath = operatorPath + operatorName + ".cs";

        //Checking if the file exists
        if (!File.Exists(copyPath))
        {
            //Logging to console
            Debug.Log("Creating Classfile: " + copyPath);

            //Using a streamwriter to make the new file
            using (StreamWriter outfile = new StreamWriter(copyPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("");
                outfile.WriteLine("public class " + operatorName + " : MachineOperator<" + operatorName + ">");
                outfile.WriteLine("{");
                outfile.WriteLine("    [SerializeField]");
                outfile.WriteLine("    private MachineMarker targetMachine = MachineMarker." + machineName + ";");
                outfile.WriteLine("");
                outfile.WriteLine("    private void Start()");
                outfile.WriteLine("    {");
                outfile.WriteLine("        Init(targetMachine);");
                outfile.WriteLine("        MachineInstance.Init(useStateNames: false);");
                outfile.WriteLine("    }");
                outfile.WriteLine("}");
            }
            //Updateing the asset database
            AssetDatabase.Refresh();

            //Closing the window
            window.Close();
        }
        else
        {
            //Showing a warning if the file exists
            WarningWindow.ShowWindow("The operator name " + operatorName + " is already in use");
        }
    }

    /// <summary>
    /// Method for making the script with comments
    /// </summary>
    private void MakeScriptWithComments()
    {
        //Setting up the path for the new file
        string copyPath = operatorPath + operatorName + ".cs";

        //Checking if the file exists
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
                outfile.WriteLine("/// The class which can operate a machine where it is allowed");
                outfile.WriteLine("/// </summary>");
                outfile.WriteLine("public class " + operatorName + " : MachineOperator<" + operatorName + ">");
                outfile.WriteLine("{");
                outfile.WriteLine("    [SerializeField]");
                outfile.WriteLine("    //The mark of the target machine, also exposed to the inspector");
                outfile.WriteLine("    private MachineMarker targetMachine = MachineMarker." + machineName + ";");
                outfile.WriteLine("");
                outfile.WriteLine("    /// <summary>");
                outfile.WriteLine("    /// Unity start method, where the machine instance is set by the init methods");
                outfile.WriteLine("    /// <summary>");
                outfile.WriteLine("    private void Start()");
                outfile.WriteLine("    {");
                outfile.WriteLine("        //Running the init of the machineoperator, to find the machine instance");
                outfile.WriteLine("        Init(targetMachine);");
                outfile.WriteLine("");
                outfile.WriteLine("        //Calling the must run method for the machine instance, and enabling the change state with types");
                outfile.WriteLine("        MachineInstance.Init(useStateNames: false);");
                outfile.WriteLine("    }");
                outfile.WriteLine("}");
            }
            //Updateing the asset database
            AssetDatabase.Refresh();

            //Closing the window
            window.Close();
        }
        else
        {
            //Showing a warning if the file exists
            WarningWindow.ShowWindow("The operator name " + operatorName + " is already in use");
        }
    }
}
