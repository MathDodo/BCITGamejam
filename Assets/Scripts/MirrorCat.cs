using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCat : MonoBehaviour
{
    private SpriteRenderer spr;
    private MeshRenderer mesh;
    private bool ghost;

    private void Start()
    {
        if (name == "GhostState")
            ghost = true;

        if (ghost)
            spr = GetComponent<SpriteRenderer>();
        else
            mesh = GetComponent<MeshRenderer>();
    }

    public void SwapCat()
    {
        if (ghost)
            spr.enabled = !spr.enabled;
        else
            mesh.enabled = !mesh.enabled;
    }
}
