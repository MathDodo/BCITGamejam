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

    private Animator dogAnimator;

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

    public void ChangeAnimation(string animationName)
    {
        dogAnimator.Play(animationName);
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
