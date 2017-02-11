using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    [SerializeField]
    private bool dontDestroyOnload = false;

    private static T instance;

    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = (T)this;

            if (dontDestroyOnload)
            {
                DontDestroyOnLoad(this);
            }
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
