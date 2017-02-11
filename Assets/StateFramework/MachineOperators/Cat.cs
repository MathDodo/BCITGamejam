
using UnityEditor;
using UnityEngine;

/// <summary>
/// The class which can operate a machine where it is allowed
/// </summary>
public class Cat : MachineOperator<Cat>
{
    [SerializeField]
    //The mark of the target machine, also exposed to the inspector
    private MachineMarker targetMachine = MachineMarker.CatFSM;

    private Animator animator;
    public Rigidbody2D Rigidbody { get; set; }
    private bool canJump;
    public GameObject hairBallPrefab;
    public Transform hairBallSpawner;


    /// <summary>
    /// Unity start method, where the machine instance is set by the init methods
    /// <summary>
    private void Start()
    {
        //Running the init of the machineoperator, to find the machine instance
        Init(targetMachine);

        //Calling the must run method for the machine instance, and enabling the change state with types
        MachineInstance.Init(useStateNames: false);

        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        MachineInstance.ChangeState<NormalState>(this);
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        //Update the active state
        MachineInstance.ExecuteActiveState(this);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectState();
        }
        if (canJump && Input.GetKeyDown(KeyCode.W))
        {
            Rigidbody.AddForce(Vector2.up * 250);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Fire(hairBallPrefab, hairBallSpawner);
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

    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Floor")
        {

            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            canJump = false;
        }
    }

    public void Fire(GameObject hairBallPrefab, Transform hairBallSpawner)
    {
        
        
        //Create the hairball!
        var hairBall = Instantiate(hairBallPrefab, hairBallSpawner.position, hairBallSpawner.rotation);


        //Add velocity
        hairBall.GetComponent<Rigidbody2D>().velocity = hairBall.transform.forward*50;
     
        Debug.Log(hairBall.transform);
        Destroy(hairBall, 5.0f);
    }

    public void ChangeAnimatorController(RuntimeAnimatorController controller)
    {
        //Enable this when we have an animator and the states have a controller
        //animator.runtimeAnimatorController = controller;
    }

    public void ChangeCollisionLayer(string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }
}

