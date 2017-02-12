using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAxe : MonoBehaviour
{

    public int damage;
    private float timerBetweenAttacks;
    public Animation anim;
   
    
	// Use this for initialization
	void Start ()
	{
	   
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	    anim.enabled = true;
        if (DimensionManager.Instance.FreezeTime && !GameObject.FindWithTag("GhostTrap"))
        {

            anim.enabled = false;
        }

	    if (timerBetweenAttacks >= 0)
	    {
	        timerBetweenAttacks -= Time.deltaTime;
	    }
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        timerBetweenAttacks = 3;


        if (other.GetComponent<Cat>().ActiveState.StateType != typeof(NormalState))
        {
            other.GetComponent<Cat>().TakeDamage(100);
        }
        else
        {
            other.GetComponent<Cat>().TakeDamage(damage);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (timerBetweenAttacks <= 0)
        {
            if (other.GetComponent<Cat>().ActiveState.StateType != typeof(NormalState))
            {
                other.GetComponent<Cat>().TakeDamage(100);
            }
            else
            {
                other.GetComponent<Cat>().TakeDamage(damage);
            }
            timerBetweenAttacks = 3;
        }
        
    }
}
