using UnityEngine;

/// <summary>
/// The specified controller for the demo operator, made so you will have a creating option in the inspector, having a controller for the specified machine,
/// and for having a specified inspector window.
/// </summary>
[CreateAssetMenu(fileName = "CatController", menuName = "StateControllers/CatController", order = 2)]
public class CatController : StateControllerGeneric<Cat>
{
    [SerializeField]
    private string test = "Test1";
}
