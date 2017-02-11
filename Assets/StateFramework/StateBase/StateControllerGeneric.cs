public abstract class StateControllerGeneric<T> : StateController where T : MachineOperator<T>
{
    /// <summary>
    /// Setting the controller operator type so there can be compared to different states which might be added
    /// </summary>
    public override void SetControllerOperatorType()
    {
        ControllerType = typeof(T);
    }
}
