using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField
{
    public Cell[,] grid;
    public Vector2Int size;
    public float radius;

    private float diameter;

    public FlowField(float radius, Vector2 size)
    {
        size = this.size;
        radius = this.radius;
        diameter = radius * 2;
    }

    public void createGrid()
    {
        grid = new Cell[size.x, size.y];
        for (int i = 0; i< size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Vector3 pos = new Vector3(diameter * i * radius, 0, diameter * j * radius);
                grid[i, j] = new Cell(pos, new Vector2Int(i, j));
            }
        }
    }
}
