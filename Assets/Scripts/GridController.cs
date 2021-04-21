using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Vector2Int size;
    public float radius = 0.5f;
    public FlowField field;
    void Start()
    {
        field = new FlowField(radius, size);
        field.createGrid();
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    Vector3 pos = new Vector3(i * radius*(radius*2), 0, j * (radius*radius*2));
                    Vector3 size = Vector3.one * radius *2;
                    Gizmos.DrawWireCube(pos,size);
                }
            }
        }
        
    }
}
