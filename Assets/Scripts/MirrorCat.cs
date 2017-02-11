using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCat : MonoBehaviour
{
    private SpriteRenderer spr;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    public void SwapCat()
    {
        spr.enabled = !spr.enabled;
    }
}
