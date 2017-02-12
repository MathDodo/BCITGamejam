using UnityEngine;

/// <summary>
/// The class which can operate a machine where it is allowed
/// </summary>
public class Cat : MachineOperator<Cat>
{
    [SerializeField]
    private int health = 100;

    [SerializeField]
    //The mark of the target machine, also exposed to the inspector
    private MachineMarker targetMachine = MachineMarker.CatFSM;

    private float xScale;
    private bool canJump;
    private Vector3 curLoc;
    private Vector3 preLoc;
    [SerializeField]
    private SpriteRenderer ghostCat;
    private MeshRenderer otherCats;

    private Animator animator;

    public GameObject hairBallPrefab;
    public Transform hairBallSpawner;

    public Rigidbody2D Rigidbody { get; set; }

    /// <summary>
    /// Unity start method, where the machine instance is set by the init methods
    /// <summary>
    private void Start()
    {
        //Running the init of the machineoperator, to find the machine instance
        Init(targetMachine);

        //Calling the must run method for the machine instance, and enabling the change state with types
        MachineInstance.Init();

        Rigidbody = GetComponent<Rigidbody2D>();
        MachineInstance.ChangeState<NormalState>(this);
        xScale = transform.localScale.x;
        otherCats = GetComponent<MeshRenderer>();

        xScale = transform.localScale.x;

        GameManager.Instance.Player = this;
        DisableGhost();
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        //Update the active state
        MachineInstance.ExecuteActiveState(this);


        InputListen();
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
        var hairBall = Instantiate(hairBallPrefab, hairBallSpawner.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        if (transform.localScale.x < 0)
        {
            hairBall.GetComponent<BulletController>().Init(-0.1f);
        }
        if (transform.localScale.x > 0)
        {
            hairBall.GetComponent<BulletController>().Init(0.1f);
        }

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

    private void InputListen()
    {
        preLoc = curLoc;

        curLoc = transform.position;
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
        //left movement
        if (Input.GetKey(KeyCode.A))
        {
            curLoc -= new Vector3(10 * Time.fixedDeltaTime, 0);

            transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
        }
        //Right movement
        if (Input.GetKey(KeyCode.D))
        {
            curLoc += new Vector3(10 * Time.fixedDeltaTime, 0);

            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        }

        transform.position = curLoc;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void EnableGhost()
    {
        ghostCat.enabled = true;
        otherCats.enabled = false;
    }

    public void DisableGhost()
    {
        ghostCat.enabled = false;
        otherCats.enabled = true;
    }
}

