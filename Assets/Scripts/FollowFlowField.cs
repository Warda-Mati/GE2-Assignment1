using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class FollowFlowField : SteeringBehavior
{
    public FlowFieldGrid flowFieldGrid; 
    public Transform target;

    public int range;
    // Start is called before the first frame update
    public float height;
    void Start()
    {
        height = target.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) < range)
        {
            float posX = Random.Range(0, flowFieldGrid.column);
            float posZ = Random.Range(0, flowFieldGrid.rows);
            target.localPosition =
                new Vector3(posX, height, posZ);
        }
    }

    public override Vector3 Calculate()
    {
        Vector3 pos = transform.localPosition;
        int x = (int)pos.x;
        int z = Mathf.RoundToInt(pos.z);
        Vector3 desired = target.position - transform.position;
        desired.Normalize();
        desired *= boid.maxSpeed;
        return desired - (flowFieldGrid.direction[x,z] * weight) ;
    }
}
