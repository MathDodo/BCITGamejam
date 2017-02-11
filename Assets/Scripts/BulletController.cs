using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    private float xTravel;
    // Use this for initialization

    public void Init(float xTravel)
    {
        this.xTravel = xTravel;
    }
	void Start ()
	{
        
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (!DimensionManager.Instance.FreezeTime)
	    {
            
           transform.Translate(new Vector3(xTravel, 0));

           
        }
       
    }
}
