using UnityEngine;

/// <summary>
/// This is a class for making specified functionality for the state,
/// and to make the creation of the scriptable object possible.
/// </summary>
[CreateAssetMenu(fileName = "GhostState", menuName = "States/GhostState", order = 1)]
public class GhostState : StateGeneric<Cat>
{
    //The name of the state also exposed for the editor
    [SerializeField]
    private string stateName = "GhostState";

    [SerializeField]
    private float exitTimer = 2;

    private float ghostTimer;

    /// <summary>
    /// Method which is called when a user enters this state, normally when the user changes states
    /// </summary>
    /// <param name="user"></param>
    public override void Enter(Cat user)
    {
        ghostTimer = exitTimer;

        user.ChangeCat("ghost");

        user.ChangeCollisionLayer("Ghost");
    }

    /// <summary>
    /// Mehtod which is called when the user wants to execute, probably an execute is called each frame
    /// </summary>
    /// <param name="user"></param>
    public override void Execute(Cat user)
    {
        //Maybe timer>??
        ghostTimer -= Time.deltaTime;

        if (ghostTimer <= 0)
        {
            user.MachineInstance.ChangeState<NormalState>(user);
        }
    }

    /// <summary>
    /// Method which is called when a user exists this state, normally when the user changes states
    /// </summary>
    /// <param name="user"></param>
    public override void Exit(Cat user)
    {
        user.ChangeCollisionLayer("Default");

        Carousel.Instance.SwapCatBack();
    }

    /// <summary>
    /// This method is run to check if this state is ready to be exited, if you want a user to be in a state for any amount time this is where you stop it from exiting
    /// </summary>
    /// <param name="user"></param>
    public override bool IsReadyToExit(Cat user)
    {
        return true;
    }

    /// <summary>
    /// Setting the state type
    /// </summary>
    public override void SetStateType()
    {
        StateType = typeof(GhostState);
    }

    /// <summary>
    /// Setting the state name
    /// </summary>
    public override void SetStateName()
    {
        StateName = stateName;
    }
}
