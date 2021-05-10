using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPath : MonoBehaviour
{
    // 4 points 
    public List<Vector3> waypoints;
    

    private void Awake()
    {
        // first point 
        
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints.Add(gameObject.transform.GetChild(i).position); 
        }
    }
    
    public void OnDrawGizmos()
    {
        //if (transform.childCount > 0) 
        //{
            Gizmos.color = Color.cyan;
            for (int i = 1; i < waypoints.Count; i++)
            {
                Vector3 prev = waypoints[i - 1];
                Vector3 next = waypoints[i % waypoints.Count];
                Gizmos.DrawLine(prev, next);
                Gizmos.DrawSphere(prev, 1);
                Gizmos.DrawSphere(next, 1);
            }
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
}
