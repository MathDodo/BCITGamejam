using UnityEngine;

/// <summary>
/// This is a class for making specified functionality for the state,
/// and to make the creation of the scriptable object possible.
/// </summary>
[CreateAssetMenu(fileName = "GravityState", menuName = "States/GravityState", order = 1)]
public class GravityState : StateGeneric<Cat>
{
    //The name of the state also exposed for the editor
    [SerializeField]
    private string stateName = "GravityState";

    [SerializeField]
    private float exitTimer = 2;

    private float gravityTimer;

    /// <summary>
    /// Method which is called when a user enters this state, normally when the user changes states
    /// </summary>
    /// <param name="user"></param>
    public override void Enter(Cat user)
    {
        user.ChangeCat("space");

        user.Rigidbody.velocity = new Vector2(0, 20);
        Physics2D.gravity = new Vector2(0, 10);

        gravityTimer = exitTimer;
        user.FlipDelayed();
    }


    /// <summary>
    /// Mehtod which is called when the user wants to execute, probably an execute is called each frame
    /// </summary>
    /// <param name="user"></param>
    public override void Execute(Cat user)
    {
        if (gravityTimer <= 0)
            user.MachineInstance.ChangeState<NormalState>(user);
        else
            gravityTimer -= Time.deltaTime;
    }

    /// <summary>
    /// Method which is called when a user exists this state, normally when the user changes states
    /// </summary>
    /// <param name="user"></param>
    public override void Exit(Cat user)
    {
        Carousel.Instance.SwapCatBack();
        user.Rigidbody.velocity = new Vector2(0, -20);
        Physics2D.gravity = new Vector2(0, -10);
        user.FlipDelayed();
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
        StateType = typeof(GravityState);
    }

    /// <summary>
    /// Setting the state name
    /// </summary>
    public override void SetStateName()
    {
        StateName = stateName;
    }
}
