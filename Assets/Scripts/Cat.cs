using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    private int dimension;

    [SerializeField]
    private int speed = 5;

    private Rigidbody2D rBody2D;

    public int Dimension { get { return dimension; } }

    private void Start()
    {
        dimension = gameObject.layer;
        rBody2D = GetComponent<Rigidbody2D>();

        rBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        transform.Translate(x, 0, 0);
    }
}
