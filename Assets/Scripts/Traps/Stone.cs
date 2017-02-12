using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{

    public int damage;
    private Transform player;
    private bool startMoving;
    public Vector2 velocity;
	// Use this for initialization
	void Start ()
	{
	    
        player = GameObject.FindWithTag("Cat").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
       
        if (!DimensionManager.Instance.FreezeTime && !GameObject.FindWithTag("GhostTrap"))
        {

            if (player.transform.position.x <= 15)
            {
                GetComponent<Rigidbody2D>().simulated = true;
            }
            if (startMoving)
            {
                velocity = new Vector2(-1, 0);
                Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
                rb2D.velocity = new Vector2(velocity.x, rb2D.velocity.y);
            }
        }
       
        
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
       

        if (collision.gameObject.tag == "Cat")
        {
            if (collision.collider.GetComponent<Cat>().ActiveState.StateType != typeof (NormalState))
            {
                collision.collider.GetComponent<Cat>().TakeDamage(100);
            }
            else
            {
                collision.collider.GetComponent<Cat>().TakeDamage(damage);
            }

        }
        if (collision.gameObject.tag == "Floor")
        {
            startMoving = true;
        }
    }
}
