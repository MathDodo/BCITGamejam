using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

/// <summary>
/// This is the state machine where you change and execute the active states of users
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class StateMachine<T> : MonoBehaviour where T : MachineOperator<T>
{
    //This field is used to save the type when you change state by type
    private Type t1;

    //This dictionary is used when you have enabled state change by types
    private Dictionary<Type, StateGeneric<T>> allowedTypeStates;

    //This dictionary is used when you have enabled state change by state names
    private Dictionary<string, StateGeneric<T>> allowedStringStates;

    //This is the specified controller which will or need to be set in a derived class
    protected StateControllerGeneric<T> specifiedController;

    //The machines mark which will be set by the derived class, and used for an operator to find the machine it wants to use
    public MachineMarker Mark { get; protected set; }

    //A static list of machines which will increase when a derived class adds itself to this list
    protected static List<StateMachine<T>> instances = new List<StateMachine<T>>();

    //A static readonly collection so you cant add new states from outside, it is used so the operator can find its machine
    public static ReadOnlyCollection<StateMachine<T>> Instances
    {
        get { return instances.AsReadOnly(); }
    }

    /// <summary>
    /// This method needs to be run before a machine is activated, and the funcionality will only be rune once.
    /// The bool is used to enable either the state change by types or by state names
    /// </summary>
    /// <param name="useStateNames"></param>
    public virtual void Init(bool useStateNames)
    {
        //Making sure the functionality can only be run once
        if (allowedTypeStates == null && allowedStringStates == null)
        {
            //If you dont want to use state names this will be run
            if (!useStateNames)
            {
                //Instantiating the dictionary
                allowedTypeStates = new Dictionary<Type, StateGeneric<T>>();

                //Adding states and types to the dictionary
                for (int i = 0; i < specifiedController.AllowedStates.Count; i++)
                {
                    specifiedController.AllowedStates[i].SetStateType();
                    allowedTypeStates.Add(specifiedController.AllowedStates[i].StateType, (StateGeneric<T>)specifiedController.AllowedStates[i]);
                }
            }
            //If you want to change by state names
            else
            {
                //Instantiating the dictionary
                allowedStringStates = new Dictionary<string, StateGeneric<T>>();

                //Adding state and state names to the dictionary
                for (int i = 0; i < specifiedController.AllowedStates.Count; i++)
                {
                    specifiedController.AllowedStates[i].SetStateType();
                    allowedStringStates.Add(specifiedController.AllowedStates[i].StateName, (StateGeneric<T>)specifiedController.AllowedStates[i]);
                }
            }
        }
    }

    /// <summary>
    /// Changing state at the user by the target state type
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="user"></param>
    public void ChangeState<T1>(T user)
    {
        //Setting t1 field to be the type of T1
        t1 = typeof(T1);

        //Cheking if the type is inside the dictionary and if the user has no active state
        if (allowedTypeStates.Keys.Contains(t1) && !user.ActiveState)
        {
            //Setting the users active state
            user.ActiveState = allowedTypeStates[t1];
            //Entering the active state
            user.ActiveState.Enter(user);
        }
        //Cheking if the type is inside the dictionary, if the users active state is not equal to the target state and if the active state is ready to be exited
        else if (allowedTypeStates.Keys.Contains(t1) && user.ActiveState != allowedTypeStates[t1] && user.ActiveState.IsReadyToExit(user))
        {
            //Exiting the active state at the user
            user.ActiveState.Exit(user);
            //Changing the active state to be the target state
            user.ActiveState = allowedTypeStates[t1];
            //Entering the new active state
            user.ActiveState.Enter(user);
        }
        //Throwing an exception if the dictionary doesnt contain the target type
        else if (!allowedTypeStates.Keys.Contains(t1))
        {
            throw new Exception("No state with the type " + t1 + " can be found in the allowed states");
        }
    }

    /// <summary>
    /// Changing the active state of the user by a state name
    /// </summary>
    /// <param name="stateName"></param>
    /// <param name="user"></param>
    public void ChangeState(string stateName, T user)
    {
        //Checking if the dictionary contains the target state name and the user doesnt have an active state
        if (allowedStringStates.Keys.Contains(stateName) && !user.ActiveState)
        {
            //Setting the users active state
            user.ActiveState = allowedTypeStates[t1];
            //Entering the active state
            user.ActiveState.Enter(user);
        }
        //Cheking if the state name is inside the dictionary, if the users active state is not equal to the target state and if the active state is ready to be exited
        else if (allowedStringStates.Keys.Contains(stateName) && user.ActiveState != allowedStringStates[stateName] && user.ActiveState.IsReadyToExit(user))
        {
            //Exiting the active state at the user
            user.ActiveState.Exit(user);
            //Changing the active state to be the target state
            user.ActiveState = allowedTypeStates[t1];
            //Entering the new active state
            user.ActiveState.Enter(user);
        }
        //Throwing an exception if the dictionary doesnt contain the target state name
        else if (!allowedStringStates.Keys.Contains(stateName))
        {
            throw new Exception("No state with the type " + t1 + " can be found in the allowed states");
        }
    }

    /// <summary>
    /// Executing the users active state
    /// </summary>
    /// <param name="user"></param>
    public void ExecuteActiveState(T user)
    {
        user.ActiveState.Execute(user);
    }
}
