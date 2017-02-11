using System.Linq;
using UnityEngine;

public class DemoMachine : StateMachine<Demo>
{
    [SerializeField]
    //This is the machines controller need to be set through the inspector
    private DemoController controller;

    [SerializeField]
    //The mark of the machine so the operator can find the machine, also exposed to the inspector
    private MachineMarker myMark = MachineMarker.DemoMachine;

    private void Awake()
    {
        //Setting the machine mark
        Mark = myMark;

        //Setting the specified controller
        specifiedController = controller;

        //Adding this machine to the instances of machines if the mark is free
        if (!instances.Any(i => i.Mark == myMark))
        {
            instances.Add(this);
        }
        else
        {
            //Destroying this machine if the instances already contains the mark
            Destroy(this);
        }
    }
}
