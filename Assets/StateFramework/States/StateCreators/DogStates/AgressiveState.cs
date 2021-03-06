using UnityEngine;

/// <summary>
/// This is a class for making specified functionality for the state,
/// and to make the creation of the scriptable object possible.
/// </summary>
[CreateAssetMenu(fileName = "AgressiveState", menuName = "States/AgressiveState", order = 1)]
public class AgressiveState : StateGeneric<Dog>
{
    [SerializeField]
    //The name of the state also exposed for the editor
    private string stateName = "AgressiveState";

    private float direction = 0;

    /// <summary>
    /// Method which is called when a user enters this state, normally when the user changes states
    /// </summary>
    /// <param name="user"></param>
    public override void Enter(Dog user)
    {
        user.ChangeAnimation("Run");
    }

    /// <summary>
    /// Mehtod which is called when the user wants to execute, probably an execute is called each frame
    /// </summary>
    /// <param name="user"></param>
    public override void Execute(Dog user)
    {
        if (GameManager.Instance.Player.transform.position.x + 1f < user.transform.position.x)
        {
            if (!user.walkingLeft)
            {
                user.Delayflip();
            }
            user.walkingLeft = true;
            direction = -75;
        }
        else if (GameManager.Instance.Player.transform.position.x - 1f > user.transform.position.x)
        {
            if (user.walkingLeft)
            {
                user.Delayflip();
            }
            user.walkingLeft = false;
            direction = 75;
        }
       
        if (user.IsTargetInAttackRange && user.AttackTimer <= 0)
        {
            user.ResetAttack();
            user.MachineInstance.ChangeState<AttackState>(user);
        }
        else if (!user.IsTargetInAttackRange)
        {
            user.RBody.velocity = new Vector2(direction * Time.deltaTime, 0);
        }
        else if (!user.IsTargetInRange)
        {
            user.MachineInstance.ChangeState<IdleState>(user);
            
        }
    }

    /// <summary>
    /// Method which is called when a user exists this state, normally when the user changes states
    /// </summary>
    /// <param name="user"></param>
    public override void Exit(Dog user)
    {
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
        StateType = typeof(AgressiveState);
    }

    /// <summary>
    /// Setting the state name
    /// </summary>
    public override void SetStateName()
    {
        StateName = stateName;
    }

}
