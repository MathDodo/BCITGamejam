using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollow : MonoBehaviour
{

    private Vector3 curLoc;
    public Transform target;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        Vector2 newTarget = new Vector2(target.transform.position.x, target.transform.position.y);
	    transform.position = Vector2.MoveTowards(transform.position, newTarget, 1f);
	}
}
