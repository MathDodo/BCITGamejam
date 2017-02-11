public class DimensionManager : Singleton<DimensionManager>
{
    //The timer for all platforms and enemies to be stopped
    public bool FreezeTime { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        //Setting the the dimension timer
        StartDimensionTime();
    }

    public void StartDimensionTime()
    {
        FreezeTime = false;
    }

    public void StopDimensionTime()
    {
        FreezeTime = true;
    }
}
