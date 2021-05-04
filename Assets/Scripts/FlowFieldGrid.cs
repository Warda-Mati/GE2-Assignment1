using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowFieldGrid : MonoBehaviour
{
    private Vector3[,] field;
    public int column, rows;
    // Start is called before the first frame update
    void Start()
    {
        field = new Vector3[column, rows];
        drawField();
    }

    void drawField()
    {
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < rows ; j++)
            {
                field[i, j] = transform.TransformPoint(new Vector3(i, transform.position.y, j));
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < rows ; j++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(transform.TransformPoint(new Vector3(i, transform.position.y, j)), Vector3.one);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
