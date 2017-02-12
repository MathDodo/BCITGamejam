using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAxe : MonoBehaviour
{

    public int damage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var anim = GetComponent<Animation>();
	    anim.enabled = true;
        if (DimensionManager.Instance.FreezeTime && !GameObject.FindWithTag("GhostTrap"))
        {

            anim.enabled = false;
        }
       
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
       


        if (other.gameObject.tag == "Cat")
        {
            if (other.collider.GetComponent<Cat>().ActiveState.StateType != typeof(NormalState))
            {
                other.collider.GetComponent<Cat>().TakeDamage(100);
            }
            else
            {
                other.collider.GetComponent<Cat>().TakeDamage(damage);
            }

        }
        
    }
}
