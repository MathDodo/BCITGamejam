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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        damage = 25;

        if (collision.gameObject.tag == "Dog")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Dog>().TakeDamage(damage);
        }
    }
}
