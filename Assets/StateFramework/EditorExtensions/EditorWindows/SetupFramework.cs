using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// This class makes an editor window and lets you change names and create scripts
/// </summary>
public class SetupFramework : EditorWindow
{
    //If you want the new scripts to be with comments
    private bool createWithComments = true;

    //The names of the different components in the framework
    private string stateName = "NewState";
    private string machineName = "NewMachine";
    private string controllerName = "NewController";
    private string operatorName = "NewMachineOperator";

    //The paths to where scripts will be
    private string operatorPath = "Assets/StateFramework/MachineOperators/";
    private string statePath = "Assets/StateFramework/States/StateCreators/";
    private string stateMachinePath = "Assets/StateFramework/StateMachines/";
    private string controllerPath = "Assets/StateFramework/StateControllers/StateControllerCreators/";

    //The window which will be shown
    private static EditorWindow window;

    /// <summary>
    /// Static method to show the window
    /// </summary>
    public static void ShowWindow()
    {
        //Making the window 
        window = GetWindow(typeof(SetupFramework));

        //Setting the size of the window
        window.maxSize = new Vector2(300, 187);
        window.minSize = window.maxSize;
    }

    /// <summary>
    /// The ongui method called by unity 
    /// </summary>
    private void OnGUI()
    {
        //Making a space in the window
        MakeSpace();

        //Enabling you to chose if you want the new scipts with comments
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

        //Enabling you to change the name of the state and showing it in the window
        stateName = EditorGUILayout.TextField("State name", stateName);

        //Making spaces
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Enabling you to change the name of the state controller and showing it in the window
        controllerName = EditorGUILayout.TextField("State controller name", controllerName);

        //Making spaces
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Enabling you to change the name of the state machine and showing it in the window
        machineName = EditorGUILayout.TextField("StateMachine name", machineName);

        //Making spaces
        for (int i = 0; i < 2; i++)
        {
            MakeSpace();
        }

        //Making the button create
        if (GUILayout.Button("Create"))
        {
            //If the button is clicked, there will be checks on whether the file names are free
            if (!File.Exists(operatorPath + operatorName + ".cs") && !File.Exists(statePath + stateName + ".cs") && !File.Exists(controllerPath + controllerName + ".cs")
                && !File.Exists(stateMachinePath + machineName + ".cs"))
            {
                if (!createWithComments)
                {
                    //Creating the scripts if the files are free
                    CreateScripts();
                }
                else
                {
                    //Creating the scripts if the files are free
                    CreateScriptsWithComments();
                }
                //Closing the window
                window.Close();
            }
            //If one or more of the file names are in use
            else
            {
                //Opening the warning window with a message
                WarningWindow.ShowWindow("One or more off the classnames is already in use");
            }
        }
    }

    /// <summary>
    /// Method to make spaces on the window
    /// </summary>
    private void MakeSpace()
    {
        EditorGUILayout.Space();
    }

    /// <summary>
    /// Method to create the scripts
    /// </summary>
    private void CreateScripts()
    {
        #region Creating Operator
        //Setting up the path for the new file
        string copyPath = operatorPath + operatorName + ".cs";

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
        #endregion

        #region Creating State
        //Changing the path
        copyPath = statePath + stateName + ".cs";

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

        #endregion

        #region Creating Controller
        //Changing the path
        copyPath = controllerPath + controllerName + ".cs";

        //Logging to console
        Debug.Log("Creating Classfile: " + copyPath);

        //Using a streamwriter to make the new file
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

        #endregion

        #region Creating StateMachine
        //Chaging the path
        copyPath = stateMachinePath + machineName + ".cs";

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
        #endregion

        //Updating the asset database with the new files
        AssetDatabase.Refresh();
    }

    private void CreateScriptsWithComments()
    {
        #region Creating Operator
        //Setting up the path for the new file
        string copyPath = operatorPath + operatorName + ".cs";

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
        #endregion

        #region Creating State
        //Changing the path
        copyPath = statePath + stateName + ".cs";

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

        #endregion

        #region Creating Controller
        //Changing the path
        copyPath = controllerPath + controllerName + ".cs";

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

        #endregion

        #region Creating StateMachine
        //Chaging the path
        copyPath = stateMachinePath + machineName + ".cs";

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
        #endregion

        //Updating the asset database with the new files
        AssetDatabase.Refresh();
    }
}

