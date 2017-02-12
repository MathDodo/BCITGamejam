using UnityEngine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;

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
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        //Update the active state
        MachineInstance.ExecuteActiveState(this);

        s = ActiveState;
        InputListen();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            canJump = false;
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

        Destroy(hairBall, 5.0f);
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

    private void InputListen()
    {
        preLoc = curLoc;

        curLoc = transform.position;
        if (canJump && Input.GetKeyDown(KeyCode.W))
        {
            Rigidbody.AddForce(Vector2.up * 250);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Fire(hairBallPrefab, hairBallSpawner);
        }
        //left movement
        if (Input.GetKey(KeyCode.A))
        {
            curLoc -= new Vector3(10 * Time.fixedDeltaTime, 0);

            transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
        }
        //Right movement
        if (Input.GetKey(KeyCode.D))
        {
            curLoc += new Vector3(10 * Time.fixedDeltaTime, 0);

            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        }

        transform.position = curLoc;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log(health);
    }
}

