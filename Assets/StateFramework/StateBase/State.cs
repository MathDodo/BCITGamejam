using System;
using UnityEngine;

/// <summary>
/// This class exists for the purpose of checking stuff when the creator windows are used
/// </summary>
public abstract class State : ScriptableObject
{
    //The type of the derived state
    public Type StateType { get; protected set; }

    //The name of the state
    public string StateName { get; protected set; }

    //The allowed operator type
    public Type OperatorType { get; protected set; }

    //Abstract method to set the state
    public abstract void SetStateType();

    //Abstract method to set the name of the state
    public abstract void SetStateName();

    //Abstract method to set the allowed operator
    public abstract void SetOperatorType();
}
