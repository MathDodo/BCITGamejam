#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CreateStateMachineWindow : EditorWindow
{
    //If you want the new scripts to be with comments
    private bool createWithComments = true;

    //The name of the new machine
    private string machineName = "NewStateMachine";

    //Name of the target controller
    private string controllerName = "StateController";

    //Allowed operatro
    private string operatorName = "MachineOperator";

    //The path of the operators
    private string operatorPath = "Assets/StateFramework/MachineOperators/";

    //The path of the state machines
    private string stateMachinePath = "Assets/StateFramework/StateMachines/";

    //The path of the controllers
    private string controllerPath = "Assets/StateFramework/StateControllers/StateControllerCreators/";

    //The window to be shown
    private static EditorWindow window;

    /// <summary>
    /// Method for making the window
    /// </summary>
    public static void ShowWindow()
    {
        //Making the window
        window = GetWindow(typeof(CreateStateMachineWindow));

        //Setting the size of the window
        window.maxSize = new Vector2(300, 160);
        window.minSize = window.maxSize;
    }

    /// <summary>
    /// The ongui method called by unity 
    /// </summary>
    private void OnGUI()
    {
        //Making a space in the window
        MakeSpace();

        //Enabling you to chose if you want the new scipt with comments
        createWithComments = EditorGUILayout.Toggle("Comments in new scripts", createWithComments);

        //Making spaces
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Enabling you to change the name of the state machine and showing it in the window
        machineName = EditorGUILayout.TextField("Machine name", machineName);

        //Making spaces
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Enabling you to change the name of the operator and showing it in the window
        operatorName = EditorGUILayout.TextField("Enabled operator", operatorName);

        //Making spaces
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Enabling you to change the name of the state controller and showing it in the window
        controllerName = EditorGUILayout.TextField("Controller name", controllerName);

        //Making spaces
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Making the create button
        if (GUILayout.Button("Create"))
        {
            //Checking if operator and controller exists
            if (File.Exists(operatorPath + operatorName + ".cs") && File.Exists(controllerPath + controllerName + ".cs"))
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
            else if (!File.Exists(operatorPath + operatorName) && !File.Exists(controllerPath + controllerName))
            {
                //Show the warning window with a message if none of the files exists
                WarningWindow.ShowWindow("Both operator name and controller name doesnt exist");
            }
            else if (!File.Exists(operatorPath + operatorName))
            {
                //Show the warning window with a message if only the operator file doesnt exist
                WarningWindow.ShowWindow("No operator with the name " + operatorName + " exists");
            }
            else if (!File.Exists(controllerPath + controllerName))
            {
                //Show the warning window with a message if only the controller file doesnt exist
                WarningWindow.ShowWindow("No controller with the name " + controllerName + " exists");
            }
        }
    }

    /// <summary>
    /// Method for making a space in the window
    /// </summary>
    private void MakeSpace()
    {
        EditorGUILayout.Space();
    }

    /// <summary>
    /// Method for making the script and updating the machine marker enum
    /// </summary>
    private void MakeScript()
    {
        //Seting the file path
        string copyPath = stateMachinePath + machineName + ".cs";

        if (!File.Exists(copyPath))
        {
            //Logging to console
            Debug.Log("Creating Classfile: " + copyPath);

            //Using a streamwriter to make the new file
            using (StreamWriter outfile = new StreamWriter(copyPath))
            {
                outfile.WriteLine("using System.Linq;");
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("");
                outfile.WriteLine("public class " + machineName + " : StateMachine<" + operatorName + ">");
                outfile.WriteLine("{");
                outfile.WriteLine("    [SerializeField]");
                outfile.WriteLine("    private " + controllerName + " controller;");
                outfile.WriteLine("");
                outfile.WriteLine("    [SerializeField]");
                outfile.WriteLine("    private MachineMarker myMark = MachineMarker." + machineName + ";");
                outfile.WriteLine("");
                outfile.WriteLine("    private void Awake()");
                outfile.WriteLine("    {");
                outfile.WriteLine("        Mark = myMark;");
                outfile.WriteLine("        specifiedController = controller;");
                outfile.WriteLine("");
                outfile.WriteLine("        if(!instances.Any(i => i.Mark == myMark))");
                outfile.WriteLine("        {");
                outfile.WriteLine("            instances.Add(this);");
                outfile.WriteLine("        }");
                outfile.WriteLine("    }");
                outfile.WriteLine("}");
            }

            //Changing the path for the enum update
            copyPath = "Assets/StateFramework/Utility/MachineMarker.cs";

            //Chaging the file with the MachineMarker enum
            //Stting up for a temp file so the change of the enum is possible
            var tempFile = Path.GetTempFileName();

            //The lines from the current file that is needed, get everything but not the closing bracket
            var linesToKeep = File.ReadAllLines(copyPath).Where(l => l != "}").ToList();

            //Adding two new lines one with the machine name as a new state of the enum and a closing bracket
            linesToKeep.Add("    " + machineName + ",");
            linesToKeep.Add("}");

            //Creating the temp file with the lines 
            File.WriteAllLines(tempFile, linesToKeep.ToArray());

            //Deleting the current file with the enum
            File.Delete(copyPath);

            //Moving the tempfile to be on the path of the deleted file
            File.Move(tempFile, copyPath);

            //Updating the asset database
            AssetDatabase.Refresh();

            //Closing window
            window.Close();
        }
        else
        {
            //Showing the warning window with a message
            WarningWindow.ShowWindow("The machine called " + machineName + " is already in use");
        }
    }

    private void MakeScriptWithComments()
    {
        //Setting the path
        string copyPath = stateMachinePath + machineName + ".cs";

        if (!File.Exists(copyPath))
        {
            //Logging to console
            Debug.Log("Creating Classfile: " + copyPath);

            //Using a streamwriter to make the new file
            using (StreamWriter outfile = new StreamWriter(copyPath))
            {
                outfile.WriteLine("using System.Linq;");
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("");
                outfile.WriteLine("public class " + machineName + " : StateMachine<" + operatorName + ">");
                outfile.WriteLine("{");
                outfile.WriteLine("    [SerializeField]");
                outfile.WriteLine("    //This is the machines controller need to be set through the inspector");
                outfile.WriteLine("    private " + controllerName + " controller;");
                outfile.WriteLine("");
                outfile.WriteLine("    [SerializeField]");
                outfile.WriteLine("    //The mark of the machine so the operator can find the machine, also exposed to the inspector");
                outfile.WriteLine("    private MachineMarker myMark = MachineMarker." + machineName + ";");
                outfile.WriteLine("");
                outfile.WriteLine("    private void Awake()");
                outfile.WriteLine("    {");
                outfile.WriteLine("        //Setting the machine mark");
                outfile.WriteLine("        Mark = myMark;");
                outfile.WriteLine("");
                outfile.WriteLine("        //Setting the specified controller");
                outfile.WriteLine("        specifiedController = controller;");
                outfile.WriteLine("");
                outfile.WriteLine("        //Adding this machine to the instances of machines if the mark is free");
                outfile.WriteLine("        if(!instances.Any(i => i.Mark == myMark))");
                outfile.WriteLine("        {");
                outfile.WriteLine("            instances.Add(this);");
                outfile.WriteLine("        }");
                outfile.WriteLine("        else");
                outfile.WriteLine("        {");
                outfile.WriteLine("            //Destroying this machine if the instances already contains the mark");
                outfile.WriteLine("            Destroy(this);");
                outfile.WriteLine("        }");
                outfile.WriteLine("    }");
                outfile.WriteLine("}");
            }

            //Changing the path
            copyPath = "Assets/StateFramework/Utility/MachineMarker.cs";

            //Chaging the file with the MachineMarker enum
            //Stting up for a temp file so the change of the enum is possible
            var tempFile = Path.GetTempFileName();

            //The lines from the current file that is needed, get everything but not the closing bracket
            var linesToKeep = File.ReadAllLines(copyPath).Where(l => l != "}").ToList();

            //Adding two new lines one with the machine name as a new state of the enum and a closing bracket
            linesToKeep.Add("    " + machineName + ",");
            linesToKeep.Add("}");

            //Creating the temp file with the lines 
            File.WriteAllLines(tempFile, linesToKeep.ToArray());

            //Deleting the current file with the enum
            File.Delete(copyPath);

            //Moving the tempfile to be on the path of the deleted file
            File.Move(tempFile, copyPath);
            //Updating the asset database
            AssetDatabase.Refresh();

            //Closing window
            window.Close();
        }
        else
        {
            //Showing the warning window with a message
            WarningWindow.ShowWindow("The machine called " + machineName + " is already in use");
        }

    }
}
#endif