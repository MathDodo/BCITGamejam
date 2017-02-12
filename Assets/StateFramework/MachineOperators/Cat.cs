using UnityEngine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The class which can operate a machine where it is allowed
/// </summary>
public class Cat : MachineOperator<Cat>
{
    [SerializeField]
    private int health = 100;

    [SerializeField]
    //The mark of the target machine, also exposed to the inspector
    private MachineMarker targetMachine = MachineMarker.CatFSM;

    private float xScale;
    private bool canJump;
    private Vector3 curLoc;
    private Vector3 preLoc;
    [SerializeField] private State s;
    [SerializeField]
    private SpriteRenderer ghostCat;

    public List<MeshRendererPair> otherCats;

    public GameObject hairBallPrefab;
    public Transform hairBallSpawner;
    public Rigidbody2D Rigidbody { get; set; }

    private Animator animator;
    private SkeletonAnimator skAnimator;

    private bool isWalking;
    private List<Animator> catimators;

    /// <summary>
    /// Unity start method, where the machine instance is set by the init methods
    /// <summary>
    private void Start()
    {
        //Running the init of the machineoperator, to find the machine instance
        Init(targetMachine);

        //Calling the must run method for the machine instance, and enabling the change state with types
        MachineInstance.Init();
        animator = GetComponent<Animator>();
        skAnimator = GetComponent<SkeletonAnimator>();

        Rigidbody = GetComponent<Rigidbody2D>();
        MachineInstance.ChangeState<NormalState>(this);

        xScale = transform.localScale.x;
        xScale = transform.localScale.x;

        GameManager.Instance.Player = this;
        catimators = new List<Animator>();
        foreach (var item in otherCats)
        {
            catimators.Add(item.rednerer.GetComponent<Animator>());
        }
        foreach (var item in Carousel.Instance.Cats)
        {
            catimators.Add(item.GetComponent<Animator>());
        }
        catimators.Add(ghostCat.GetComponent<Animator>());
        catimators.Add(Carousel.Instance.normalCat.GetComponent<Animator>());

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
        if (hit != null && hit.Where(x => x.collider.gameObject.tag == "Floor").Any())
        {
            if (isJumping)
                isJumping = false;

            canJump = true;
        }

        InputListen();
        SetBoolInAnimators();
    }

    private void InputListen()
    {
        preLoc = curLoc;
        curLoc = transform.position;

        if (canJump && Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            Rigidbody.AddForce(Vector2.up * 250);
        }

        isWalking = false;

        //left movement
        if (Input.GetKey(KeyCode.A))
        {
            curLoc -= new Vector3(10 * Time.fixedDeltaTime, 0);
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
            curLoc += new Vector3(10 * Time.fixedDeltaTime, 0);
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
    private bool isJumping;
    private void SetBoolInAnimators()
    {
        foreach (var item in catimators)
        {
            item.SetBool("jumping", isJumping);
            item.SetBool("walking", isWalking);
        }
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
            Destroy(gameObject);
        }
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
}

