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
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
       


        if (other.gameObject.tag == "Player")
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
