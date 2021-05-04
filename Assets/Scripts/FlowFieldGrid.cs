using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowFieldGrid : MonoBehaviour
{
    public Vector2[,] field;
    public Vector3[,] direction;
    public int column, rows;
    // Start is called before the first frame update
    void Start()
    {
        field = new Vector2[column, rows];
        direction = new Vector3[column, rows];
        drawField();
    }

    void drawField()
    {
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < rows ; j++)
            {
                field[i, j] = transform.TransformPoint(new Vector3(i,j));
                direction[i,j] = Vector3.forward;
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
        Debug.Log(direction[15,6]);
    }
}
