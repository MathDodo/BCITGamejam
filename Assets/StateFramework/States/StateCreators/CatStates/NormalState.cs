using UnityEngine;

/// <summary>
/// This is a class for making specified functionality for the state,
/// and to make the creation of the scriptable object possible.
/// </summary>
[CreateAssetMenu(fileName = "NormalState", menuName = "States/NormalState", order = 1)]
public class NormalState : StateGeneric<Cat>
{
    //The name of the state also exposed for the editor
    [SerializeField]
    private string stateName = "NormalState";

    private GameObject hairBallPrefab;
    private Transform hairBallSpawner;

    private float timer;
    public float attackTimer = 5f;

    /// <summary>
    /// Method which is called when a user enters this state, normally when the user changes states
    /// </summary>
    /// <param name="user"></param>
    public override void Enter(Cat user)
    {
        user.ChangeCat("normal");
        user.ChangeCollisionLayer("Default");
        hairBallSpawner = user.hairBallSpawner;
        hairBallPrefab = user.hairBallPrefab;
        timer = attackTimer;
    }


    /// <summary>
    /// Mehtod which is called when the user wants to execute, probably an execute is called each frame
    /// </summary>
    /// <param name="user"></param>
    public override void Execute(Cat user)
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.UpArrow) && timer <= 0)
        {
            timer = attackTimer;
            user.Fire(hairBallPrefab, hairBallSpawner);
        }

    }

    /// <summary>
    /// Method which is called when a user exists this state, normally when the user changes states
    /// </summary>
    /// <param name="user"></param>
    public override void Exit(Cat user)
    {
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
        StateType = typeof(NormalState);
    }

    /// <summary>
    /// Setting the state name
    /// </summary>
    public override void SetStateName()
    {
        StateName = stateName;
    }
}
