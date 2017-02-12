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
	    
        player = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (player.transform.position.x <= 100)
	    {
            GetComponent<Rigidbody2D>().simulated = true;
        }
	    if (startMoving)
	    {
	        velocity = new Vector2(-10,0);
	        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
            rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
	    }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
       

        if (collision.gameObject.tag == "Player")
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
