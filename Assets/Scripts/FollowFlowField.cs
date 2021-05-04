using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFlowField : SteeringBehavior
{
    public FlowFieldGrid flowFieldGrid;
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
        return flowFieldGrid.direction[x,z] ;
    }
}
