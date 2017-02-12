using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCat : MonoBehaviour
{
    private SpriteRenderer spr;

    private void Start()
    {
       
    }

    public void SwapCat()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.enabled = !spr.enabled;
    }
}
