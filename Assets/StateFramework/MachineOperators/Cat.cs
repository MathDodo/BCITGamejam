using UnityEngine;

/// <summary>
/// The class which can operate a machine where it is allowed
/// </summary>
public class Cat : MachineOperator<Cat>
{
    [SerializeField]
    //The mark of the target machine, also exposed to the inspector
    private MachineMarker targetMachine = MachineMarker.CatFSM;

<<<<<<< HEAD
    [SerializeField]
    private RuntimeAnimatorController c;

    public Animator ting { get; private set; }
=======
    private Animator animator;
    public Rigidbody2D Rigidbody { get; set; }
>>>>>>> 2ece3ce0f65d1f607700ce65f3a372399e78898b

    /// <summary>
    /// Unity start method, where the machine instance is set by the init methods
    /// <summary>
    private void Start()
    {
        //Running the init of the machineoperator, to find the machine instance
        Init(targetMachine);

        //Calling the must run method for the machine instance, and enabling the change state with types
        MachineInstance.Init(useStateNames: false);

<<<<<<< HEAD
=======
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
>>>>>>> 2ece3ce0f65d1f607700ce65f3a372399e78898b
        MachineInstance.ChangeState<NormalState>(this);
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        //Update the active state
        MachineInstance.ExecuteActiveState(this);
<<<<<<< HEAD
=======

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectState();
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 500;
        Rigidbody.velocity = new Vector2(x, Rigidbody.velocity.y);
    }

    /// <summary>
    /// Select the state to go to here
    /// </summary>
    private void SelectState()
    {
        // if we are not in the normal state return
        if (!(ActiveState is NormalState))
            return;

        //Get the state in the carousel

        MachineInstance.ChangeState<GravityState>(this);
>>>>>>> 2ece3ce0f65d1f607700ce65f3a372399e78898b
    }
}