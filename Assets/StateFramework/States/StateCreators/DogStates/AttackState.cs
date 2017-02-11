using UnityEngine;

/// <summary>
/// This is a class for making specified functionality for the state,
/// and to make the creation of the scriptable object possible.
/// </summary>
[CreateAssetMenu(fileName = "AttackState", menuName = "States/AttackState", order = 1)]
public class AttackState : StateGeneric<Dog>
{
    [SerializeField]
    //The name of the state also exposed for the editor
    private string stateName = "AttackState";

    /// <summary>
    /// Method which is called when a user enters this state, normally when the user changes states
    /// </summary>
    /// <param name="user"></param>
    public override void Enter(Dog user)
    {
        user.ChangeAnimation("Attack");
    }

    /// <summary>
    /// Mehtod which is called when the user wants to execute, probably an execute is called each frame
    /// </summary>
    /// <param name="user"></param>
    public override void Execute(Dog user)
    {
        user.AttackDelay -= Time.deltaTime;

        if (user.AttackDelay <= 0 && user.IsTargetInAttackRange)
        {
            //Deal damage to target
        }
        else if (!user.IsTargetInAttackRange)
        {

        }
    }

    /// <summary>
    /// Method which is called when a user exists this state, normally when the user changes states
    /// </summary>
    /// <param name="user"></param>
    public override void Exit(Dog user)
    {
        user.ResetAttack();
    }

    /// <summary>
    /// This method is run to check if this state is ready to be exited, if you want a user to be in a state for any amount time this is where you stop it from exiting
    /// </summary>
    /// <param name="user"></param>
    public override bool IsReadyToExit(Dog user)
    {
        return true;
    }

    /// <summary>
    /// Setting the state type
    /// </summary>
    public override void SetStateType()
    {
        StateType = typeof(AttackState);
    }

    /// <summary>
    /// Setting the state name
    /// </summary>
    public override void SetStateName()
    {
        StateName = stateName;
    }
}
