using UnityEngine;

/// <summary>
/// The class which can operate a machine where it is allowed
/// </summary>
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

    private Animator dogAnimator;

    public float AttackDelay { get; set; }
    public float AttackTimer { get; private set; }
    public bool IsTargetInRange { get; set; }
    public bool IsTargetInAttackRange { get; set; }

    /// <summary>
    /// Unity start method, where the machine instance is set by the init methods
    /// <summary>
    private void Start()
    {
        //Running the init of the machineoperator, to find the machine instance
        Init(targetMachine);

        //Calling the must run method for the machine instance, and enabling the change state with types
        MachineInstance.Init();

        dogAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (AttackTimer >= 0)
        {
            AttackTimer -= Time.deltaTime;
        }

        if (!DimensionManager.Instance.FreezeTime)
        {
            dogAnimator.speed = 1;
        }
        else
        {
            //Not sure about this one
            dogAnimator.speed = 0;
        }
    }

    public void ResetAttack()
    {
        AttackTimer = timeBetweenAttacks;
        AttackDelay = attackDelay;
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
            Destroy(this);
        }
    }
}
