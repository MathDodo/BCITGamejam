using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Carousel : Singleton<Carousel>
{
    [SerializeField]
    private Cat player;


    private List<Vector3> catPos;
    private List<Vector2> catScales;
    private List<MirrorCat> Cats = new List<MirrorCat>();
    private MirrorCat normalCat;
    private bool canRotate;

    public MirrorCat CurrentCat { get; set; }

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        catPos = new List<Vector3> { new Vector2(-1.7f, -.5f), new Vector2(0, -.5f), new Vector2(1.7f, -.5f) };
        catScales = new List<Vector2>() { new Vector2(.3f, -.3f), new Vector2(.5f, -.5f), new Vector2(.3f, -.3f) };


        //Get all the children
        foreach (Transform item in transform)
        {
            if (item.name != "NormalState")
                Cats.Add(item.GetComponent<MirrorCat>());
            else
                normalCat = item.GetComponent<MirrorCat>();
        }

        canRotate = true;
        normalCat.SwapCat();
        CurrentCat = Cats.Find(x => x.transform.localPosition == catPos[1]);
    }

    /// <summary>
    /// Update
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Rotate(-1);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Rotate(1);
        if (Input.GetKeyDown(KeyCode.Space))
            SwapCat();

        transform.position = new Vector2(player.transform.position.x, transform.position.y);
    }

    /// <summary>
    /// Swaps the normal and the current cat
    /// </summary>
    public void SwapCat()
    {
        normalCat.SwapCat();
        CurrentCat.SwapCat();
        player.MachineInstance.ChangeState(CurrentCat.name, player);
        canRotate = false;
    }

    /// <summary>
    /// Swaps the current cat back and hides the normal cat
    /// </summary>
    public void SwapCatBack()
    {
        normalCat.SwapCat();
        CurrentCat.SwapCat();
        canRotate = true;
    }

    /// <summary>
    /// Rotate the carousel in a direction
    /// </summary>
    /// <param name="shift">negative value for rotating left</param>
    public void Rotate(int shift)
    {
        if (canRotate)
            foreach (var item in Cats)
            {
                var currPosIndex = catPos.IndexOf(item.transform.localPosition);

                item.transform.localPosition = catPos[ShiftInts(currPosIndex, shift, Cats.Count - 1, 0)];
                item.transform.localScale = catScales[ShiftInts(currPosIndex, shift, Cats.Count - 1, 0)];
            }

        //Set the current cat to the middle one
        CurrentCat = Cats.Find(x => x.transform.localPosition == catPos[1]);
    }

    /// <summary>
    /// Shifts an int an wraps around if it goes over the given max or min
    /// </summary>
    /// <param name="current">The values to change</param>
    /// <param name="shift">The shift to apply (only 1 at a time right now)</param>
    /// <param name="max">The max</param>
    /// <param name="min">the min</param>
    /// <returns></returns>
    private int ShiftInts(int current, int shift, int max, int min)
    {
        //Make it take account of more than 1 at a time
        current += shift;

        if (current > max)
            current = min;
        else if (current < min)
            current = max;

        return current;
    }
}
