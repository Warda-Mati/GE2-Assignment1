using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowFieldGrid : MonoBehaviour
{
    public Vector2[,] field;
    public Vector3[,] direction;
    public int column, rows;

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        field = new Vector2[column, rows];
        direction = new Vector3[column, rows];
        drawField();
    }

    void drawField()
    {
        float offsetx = 0;
        for (int i = 0; i < column; i++)
        {
            float offsety = 0;
            for (int j = 0; j < rows ; j++)
            {
                field[i, j] = transform.TransformPoint(new Vector3(i,j));
                float theta = map(Mathf.PerlinNoise(offsetx, offsety), 0, 1, 0, Mathf.PI * 2);
                direction[i, j] = new Vector3(Mathf.Cos(theta),0,Mathf.Sin(theta));
                offsety += 0.1f;
            }

            offsetx += 0.1f;
        }
    }

    private void OnDrawGizmos()
    {
        //if (!Application.isPlaying)
        //{
        //    field = new Vector2[column, rows];
        //    direction = new Vector3[column, rows];
        if (Application.isPlaying)
        {
            float offsetx = 0;
            for (int i = 0; i < column; i++)
            {
                float offsety = 0;
                for (int j = 0; j < rows ; j++)
                {
                    field[i, j] = transform.TransformPoint(new Vector3(i,j));
                    float theta = map(Mathf.PerlinNoise(offsetx, offsety), 0, 1, 0, Mathf.PI * 2);
                    direction[i, j] = new Vector3(Mathf.Cos(theta),0,Mathf.Sin(theta));
                    offsety += 0.1f;
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(transform.TransformPoint(new Vector3(i, transform.position.y, j)), Vector3.one);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawRay(transform.TransformPoint(new Vector3(i, transform.position.y, j)),direction[i,j]);
                    
                }

                offsetx += 0.1f;
            }
            
        }
          
        //}
    }
    
    
    public float map (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
