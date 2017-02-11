using UnityEngine;

/// <summary>
/// This class is the operator base class which is generic on the derived operator
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MachineOperator<T> : MonoBehaviour where T : MachineOperator<T>
{
    //This is the active state which will be executed by a StateMachine
    public StateGeneric<T> ActiveState { get; set; }

    //The instance of the machine which have been found the derived operator will use this to change and execute states
    public StateMachine<T> MachineInstance { get; private set; }

    /// <summary>
    /// This function will need to be called by the derived class before the Machine instance will work, the function uses a mark to find the target StateMachine
    /// </summary>
    /// <param name="mark"></param>
    protected void Init(MachineMarker mark)
    {
        //Setting the machine instance through the static list in the StateMachine<T>, execute Init in the Start function because an instance will be set in Awake
        MachineInstance = StateMachine<T>.Instances.Find(i => i.Mark == mark);
    }
}
