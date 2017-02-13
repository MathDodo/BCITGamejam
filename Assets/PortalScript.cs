using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{

    private void Awake()
    {
        GetComponent<Animator>().speed = 2f;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
