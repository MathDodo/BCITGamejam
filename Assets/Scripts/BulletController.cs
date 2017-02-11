using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float xTravel;
    private int damage;
    // Use this for initialization

    public void Init(float xTravel)
    {
        this.xTravel = xTravel;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!DimensionManager.Instance.FreezeTime)
        {
            transform.Translate(new Vector3(xTravel, 0));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this);
        damage = 25;
        if (other.gameObject.tag == "Dog")
        {
            other.gameObject.GetComponent<Dog>().TakeDamage(damage);
        }
    }
}
