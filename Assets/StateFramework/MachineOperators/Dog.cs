using UnityEngine;

/// <summary>
/// The class which can operate a machine where it is allowed
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Dog : MachineOperator<Dog>
{
    [SerializeField]
    //The mark of the target machine, also exposed to the inspector
    private MachineMarker targetMachine = MachineMarker.DogMachine;

    [SerializeField]
    private int health = 100;

    [SerializeField]
    private float timeBetweenAttacks;

    [SerializeField]
    private float attackDelay = 0.3f;

    [SerializeField]
    private float idleTime = 2.5f;

    [SerializeField]
    private float movingTime = 2;

    [SerializeField]
    private float startMovingDirection = 1;

    [SerializeField]
    private float targetingDistance = 15;

    [SerializeField]
    private float attackDistance = 5f;

    [SerializeField]
    private State activeState;

    [SerializeField]
    private int damage = 10;

    private Animator dogAnimator;

    public float IdleTimer { get; set; }
    public float MovingTimer { get; set; }
    public float AttackDelay { get; set; }
    public float AttackTimer { get; private set; }

    public bool IsTargetInRange { get; set; }
    public Rigidbody2D RBody { get; private set; }
    public bool IsTargetInAttackRange { get; set; }
    public float MovingDirection { get; private set; }
    public int Damage { get { return damage; } }
    /// <summary>
    /// Unity start method, where the machine instance is set by the init methods
    /// <summary>
    private void Start()
    {
        //Running the init of the machineoperator, to find the machine instance
        Init(targetMachine);

        //Calling the must run method for the machine instance, and enabling the change state with types
        MachineInstance.Init();

        MachineInstance.ChangeState<IdleState>(this);

        dogAnimator = GetComponent<Animator>();

        RBody = GetComponent<Rigidbody2D>();

        MovingDirection = startMovingDirection;
        IdleTimer = idleTime;
        MovingTimer = movingTime;
    }

    private void Update()
    {
        activeState = ActiveState;
        if (!DimensionManager.Instance.FreezeTime)
        {
            NotFrozenUpdate();
        }
        else
        {
            //Not sure about this one
            dogAnimator.speed = 0;
        }
    }

    private void NotFrozenUpdate()
    {
        //dogAnimator.speed = 1;
        if (GameManager.Instance.Player)
        {
            IsTargetInRange = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) <= targetingDistance;
            IsTargetInAttackRange = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) <= attackDistance;
        }

        if (AttackTimer >= 0)
        {
            AttackTimer -= Time.deltaTime;
        }

        MachineInstance.ExecuteActiveState(this);
    }

    public void ResetAttack()
    {
        AttackTimer = timeBetweenAttacks;
        AttackDelay = attackDelay;
        IsTargetInAttackRange = false;
        IsTargetInRange = false;
    }

    public void ResetIdleTimer()
    {
        IdleTimer = idleTime;
    }

    public void ResetMovingTimer()
    {
        MovingTimer = movingTime;
        MovingDirection *= -1;
    }

    public void ChangeAnimation(string animationName)
    {
        //Do this when animator is made
        //dogAnimator.Play(animationName);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
