using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedicateWall : MonoBehaviour
{
    BoxCollider2D col;
    public LayerMask wall;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }
}
