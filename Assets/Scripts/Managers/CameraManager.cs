using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField]
    private Cat target;

    [SerializeField]
    private float yOffset = 3;

    private Camera cam;
    private Transform targetTransform;

    public float YOffset { get { return yOffset; } set { yOffset = value; } }

    // Use this for initialization
    private void Start()
    {
        cam = GetComponent<Camera>();
        targetTransform = target.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (target)
            transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y + yOffset, -10);
    }
}
