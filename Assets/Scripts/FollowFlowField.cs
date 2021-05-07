using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFlowField : SteeringBehavior
{
    public FlowFieldGrid flowFieldGrid; 
    public Transform target;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override Vector3 Calculate()
    {
        Vector3 pos = transform.localPosition;
        int x = (int)pos.x;
        int z = Mathf.RoundToInt(pos.z);
        Vector3 desired = target.position - transform.position;
        desired.Normalize();
        desired *= boid.maxSpeed;
        Debug.Log("flowfield is " + flowFieldGrid.direction[x,z]);
        Debug.Log("desired is " + desired);
        Debug.Log("together is " + (desired - flowFieldGrid.direction[x,z]));
        return desired - (flowFieldGrid.direction[x,z] * weight) ;
    }
}
