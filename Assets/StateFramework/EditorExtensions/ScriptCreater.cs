#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>
/// This script enables the unity editor extensions in the StateFramework which you can see in editor top bar
/// </summary>
public class ScriptCreater : MonoBehaviour
{
    /// <summary>
    /// This method opens the window which will let you setup a framework, and it enables the stateframework create all button
    /// </summary>
    [MenuItem("StateFramework/CreateAll")]
    public static void CreateFramework()
    {
        SetupFramework.ShowWindow();
    }

    /// <summary>
    /// This method opens the window where you can create an operator, and enables the stateframework create individual and machine operator button
    /// </summary>
    [MenuItem("StateFramework/CreateIndividual/MachineOperator")]
    public static void CreateStateMachineHandler()
    {
        CreateOperatorWindow.ShowWindow();
    }

    /// <summary>
    /// This method opens the window where you can create a new state, and enables the stateframework create individual and state button
    /// </summary>
    [MenuItem("StateFramework/CreateIndividual/State")]
    public static void CreateState()
    {
        CreateStateWindow.ShowWindow();
    }

    /// <summary>
    /// This method opens the window where you can create a new state controller, and enables the stateframework create individual and state controller button
    /// </summary>
    [MenuItem("StateFramework/CreateIndividual/StateController")]
    public static void CreateScriptableObject()
    {
        CreateStateControllerWindow.ShowWindow();
    }

    /// <summary>
    /// This method opens the window where you can create a new state machine, and enables the stateframework create individual and state machine button
    /// </summary>
    [MenuItem("StateFramework/CreateIndividual/StateMachine")]
    public static void CreateStateMachine()
    {
        CreateStateMachineWindow.ShowWindow();
    }
}
#endif


