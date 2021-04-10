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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
}
