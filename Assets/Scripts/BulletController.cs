using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float xTravel;
    private int damage;
    private float timer = 5;
    // Use this for initialization

    public void Init(float xTravel)
    {
        this.xTravel = xTravel;
    }

    // Update is called once per frame
    void Update()
    {
        if (!DimensionManager.Instance.FreezeTime)
        {
            transform.Translate(new Vector3(xTravel * 5, 0));

            timer -= Time.deltaTime;
            if (timer <= 0)
                Destroy(gameObject);
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
