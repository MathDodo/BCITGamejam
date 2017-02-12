/// <summary>
/// This class exists to set the type of the allowed operator, and to setup the state pattern
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class StateGeneric<T> : State where T : MachineOperator<T>
{
    /// <summary>
    /// Setting the allowed operator
    /// </summary>
    public override void SetOperatorType()
    {
        //Setting the operator type to the type of T which is the type of the allowed operator
        OperatorType = typeof(T);
    }

    /// <summary>
    /// The enter method from the state pattern, this will be called once each time an operator enters the derived state
    /// </summary>
    /// <param name="user"></param>
    public abstract void Enter(T user);

    /// <summary>
    /// The execute method from the state pattern, this will be called each time you execute the active state
    /// </summary>
    /// <param name="user"></param>
    public abstract void Execute(T user);

    /// <summary>
    /// The exit method from  the state pattern, this will be called when the operator changes to another state
    /// </summary>
    /// <param name="user"></param>
    public abstract void Exit(T user);

    /// <summary>
    /// This method will be used to check if the user is ready to leave the active state
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public abstract bool IsReadyToExit(T user);
}
