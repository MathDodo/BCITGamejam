using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class exists for the purpose of checking stuff when the creator windows are used.
/// A custom inspector window will be created to the specific controller.
/// </summary>
public abstract class StateController : ScriptableObject
{
    //The list of allowed states in this controller, will be showed by a custom inspector view no need to use Serialize field
    private List<State> allowedStates = new List<State>();

    //Making the access public for the allowed states but as read only so you cant remove or add to the allowed states through code
    public ReadOnlyCollection<State> AllowedStates
    {
        get { return allowedStates.AsReadOnly(); }
    }

    //The type of the allowed operator 
    public Type ControllerType { get; protected set; }

    /// <summary>
    /// Abstract method to set the allowed operator in a derived class
    /// </summary>
    public abstract void SetControllerOperatorType();

    /// <summary>
    /// Method to add states through the controllers add state window it shouldnt be used at run time
    /// </summary>
    /// <param name="state"></param>
    public void AddState(State state)
    {
        //Making sure the application is in editor mode before adding states
        if (Application.isEditor && !Application.isPlaying)
        {
            //Adding the new state
            allowedStates.Add(state);
        }
        else
        {
            //Throwing an exception if you try to do something at run time
            throw new InvalidOperationException("Cant add states at run time");
        }
    }

    /// <summary>
    /// Method to remove states by name through the controllers remove state window it shouldnt be used at run time
    /// </summary>
    /// <param name="name"></param>
    public void RemoveState(string name)
    {
        //Making sure the application is in editor mode before removing states
        if (Application.isEditor && !Application.isPlaying)
        {
            //Checking if the state you want to remove exists in the list
            if (allowedStates.Any(s => s.StateName == name))
            {
                //Removing the state
                allowedStates.Remove(allowedStates.Find(s => s.StateName == name));
            }
            else
            {
                //Throwing an exception if there is no state by the name
                throw new Exception("There is no state with the name " + name);
            }
        }
        else
        {
            //Throwing an exception if you try to do something at run time
            throw new InvalidOperationException("Cant remove states at rune time");
        }
    }

    /// <summary>
    /// Method to remove all objects in the list of allowed states it shouldnt be used at run time
    /// </summary>
    public void ClearStates()
    {
        //Making sure the application is in editor mode before clearing states
        if (Application.isEditor && !Application.isPlaying)
        {
            //Removing all states
            allowedStates.Clear();
        }
        else
        {
            //Throwing an exception if you try to do something at run time
            throw new InvalidOperationException("Cant clear states at rune time");
        }
    }
}

