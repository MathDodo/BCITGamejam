using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTraps : MonoBehaviour
{

    public int damage;
	// Use this for initialization
	void Start ()
    {
		
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cat")
        {
            if (other.GetComponent<Cat>().ActiveState.StateType != typeof(NormalState))
            {
                other.GetComponent<Cat>().TakeDamage(100);
            }
            else
            {
                other.GetComponent<Cat>().TakeDamage(damage);
            }

        }
    }
}
