using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The class which can operate a machine where it is allowed
/// </summary>
public class Cat : MachineOperator<Cat>
{
    [SerializeField]
    private int maxHealth = 100;

    private int health;

    [SerializeField]
    //The mark of the target machine, also exposed to the inspector
    private MachineMarker targetMachine = MachineMarker.CatFSM;

    [SerializeField]
    private SpriteRenderer ghostCat;

    [SerializeField]
    private int lives = 9;

    public List<MeshRendererPair> otherCats;
    public GameObject hairBallPrefab;
    public Transform hairBallSpawner;
    public float jumpForce;

    private Animator animator;
    private float xScale;
    private Vector3 curLoc;
    private Vector3 preLoc;
    private SkeletonAnimator skAnimator;

    private bool isWalking;
    private bool isJumping;
    private bool canJump;
    private bool isFalling;

    private float jumpTimer;
    public float jumpDelay;

    public float speed;
    private List<SkeletonAnimation> catimators;

    public Rigidbody2D Rigidbody { get; set; }

    public int Health { get { return health; } }
    public int Lives { get { return lives; } }

    /// <summary>
    /// Unity start method, where the machine instance is set by the init methods
    /// <summary>
    private void Start()
    {
        health = maxHealth;
        //Running the init of the machineoperator, to find the machine instance
        Init(targetMachine);

        //Calling the must run method for the machine instance, and enabling the change state with types
        MachineInstance.Init();
        animator = GetComponent<Animator>();
        skAnimator = GetComponent<SkeletonAnimator>();

        Rigidbody = GetComponent<Rigidbody2D>();
        MachineInstance.ChangeState<NormalState>(this);

        xScale = transform.localScale.x;

        GameManager.Instance.Player = this;
        catimators = new List<SkeletonAnimation>();
        foreach (var item in otherCats)
        {
            var anim = item.rednerer.GetComponent<SkeletonAnimation>();
            if (anim != null)
                catimators.Add(anim);
        }
        foreach (var item in Carousel.Instance.Cats)
        {
            var anim = item.GetComponent<SkeletonAnimation>();
            if (anim != null)
                catimators.Add(anim);
        }
        catimators.Add(Carousel.Instance.normalCat.GetComponent<SkeletonAnimation>());

    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        //Update the active state
        MachineInstance.ExecuteActiveState(this);

        canJump = false;

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.down, .05f);
        if (hit != null && hit.Where(x => x.collider.gameObject.tag == "Floor").Any() && jumpTimer <= 0)
        {
            canJump = true;

            if (isJumping)
                isJumping = false;
        }

        isFalling = Rigidbody.velocity.y < 0;

        if (jumpTimer > 0)
            jumpTimer -= Time.deltaTime;

        InputListen();
        SetBoolInAnimators();
    }

    private void SetBoolInAnimators()
    {
        foreach (var item in catimators)
        {
            //If the player is idleing
            if (!isWalking && !isJumping && !isFalling && canJump)
            {
                item.AnimationName = "idle";
                item.loop = true;
            }

            //If the player is waling on the ground
            if (isWalking && !isJumping && !isFalling && canJump)
            {
                item.AnimationName = "walk";
                item.loop = true;
            }

            //if the player is jumping
            if (isJumping)
            {
                item.AnimationName = "jump_up";
                item.loop = false;
            }
            //if the player is falling
            if (isFalling)
            {
                item.AnimationName = "jump_falling";
                item.loop = true;
            }
        }
    }

    private void InputListen()
    {
        preLoc = curLoc;
        curLoc = transform.position;

        if (canJump && Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            Rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        isWalking = false;

        //left movement
        if (Input.GetKey(KeyCode.A))
        {
            curLoc -= new Vector3(speed * Time.fixedDeltaTime, 0);
            isWalking = true;
            transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);

            for (int i = 0; i < Carousel.Instance.Cats.Count; i++)
            {
                if (Carousel.Instance.Cats[i].transform.localScale.x > 0)
                {
                    Carousel.Instance.Cats[i].transform.localScale = new Vector3(Carousel.Instance.Cats[i].transform.localScale.x * -1, Carousel.Instance.Cats[i].transform.localScale.y,
                        transform.localScale.z);
                    Carousel.Instance.normalCat.transform.localScale = new Vector3(Carousel.Instance.normalCat.transform.localScale.x * -1, Carousel.Instance.normalCat.transform.localScale.y);

                }
            }
        }
        //Right movement
        if (Input.GetKey(KeyCode.D))
        {
            curLoc += new Vector3(speed * Time.fixedDeltaTime, 0);
            isWalking = true;
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);

            for (int i = 0; i < Carousel.Instance.Cats.Count; i++)
            {
                if (Carousel.Instance.Cats[i].transform.localScale.x < 0)
                {
                    Carousel.Instance.Cats[i].transform.localScale = new Vector3(Carousel.Instance.Cats[i].transform.localScale.x * -1, Carousel.Instance.Cats[i].transform.localScale.y,
                        transform.localScale.z);
                    Carousel.Instance.normalCat.transform.localScale = new Vector3(Carousel.Instance.normalCat.transform.localScale.x * -1, Carousel.Instance.normalCat.transform.localScale.y);
                }
            }
        }

        transform.position = curLoc;
    }

    public void Fire(GameObject hairBallPrefab, Transform hairBallSpawner)
    {
        //Create the hairball!
        var hairBall = Instantiate(hairBallPrefab, hairBallSpawner.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        if (transform.localScale.x < 0)
        {
            hairBall.GetComponent<BulletController>().Init(-0.1f);
        }
        if (transform.localScale.x > 0)
        {
            hairBall.GetComponent<BulletController>().Init(0.1f);
        }
    }

    public void ChangeCat(string name)
    {
        if (name == "ghost")
        {
            ghostCat.enabled = true;

            foreach (var item in otherCats)
            {
                item.rednerer.enabled = false;
            }
        }

        else
        {
            ghostCat.enabled = false;

            foreach (var item in otherCats)
            {
                if (item.name == name)
                    item.rednerer.enabled = true;
                else
                    item.rednerer.enabled = false;
            }
        }

    }

    public void ChangeCollisionLayer(string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }


    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            if (lives > 0)
            {
                Respawn();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        Debug.Log(health);
    }

    public void FlipDelayed()
    {
        StartCoroutine(FlipScale());
    }

    private IEnumerator FlipScale()
    {
        yield return new WaitForSeconds(.03f);

        Vector3 userScaler = transform.localScale;
        userScaler.y *= -1;
        transform.localScale = userScaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hole")
        {
            TakeDamage(health);
        }
        else if (collision.gameObject.tag == "DeadlyCeiling")
        {
            TakeDamage(5);
        }
    }

    private void Respawn()
    {
        health = maxHealth;
        MachineInstance.ChangeState<NormalState>(this);
        transform.position = new Vector3(-5.18f, -2.14f, 15);
    }
}

