using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum CatStates
{
    normal, ghost, time, gravity
}

public class Carousel : MonoBehaviour
{
    private List<GameObject> Cats = new List<GameObject>();

    public GameObject CurrentCat { get; set; }


    [SerializeField]
    private Cat player;
    private CatStates state;
    private List<Vector2> catPos = new List<Vector2> { new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1) };
    private List<Vector2> catScales = new List<Vector2> { new Vector2(.5f, .5f), new Vector2(1, 1), new Vector2(.5f, .5f) };

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        //Get all the children
        foreach (Transform item in transform)
        {
            if (item.name != "normal")
                Cats.Add(item.gameObject);
        }
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
    }

    /// <summary>
    /// Rotate the carousel in a direction
    /// </summary>
    /// <param name="shift">negative value for rotating left</param>
    public void Rotate(int shift)
    {
        foreach (var item in Cats)
        {
            var currPosIndex = catPos.IndexOf(item.transform.localPosition);

            item.transform.localPosition = catPos[ShiftInts(currPosIndex, shift, Cats.Count - 1, 0)];
            item.transform.localScale = catScales[ShiftInts(currPosIndex, shift, Cats.Count - 1, 0)];
        }
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
