using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : SteeringBehavior
{
    public FishPath path;
    private int counter = 0;
    public override Vector3 Calculate()
    {
        Vector3 f = Vector3.zero;
            
        f += boid.SeekForce(path.waypoints[counter]);
        f += boid.ArriveForce(path.waypoints[counter]);


        if (Vector3.Distance( path.waypoints[counter],transform.position) < 0.5f)
        {
            counter = (counter+1) % path.waypoints.Count;
        }

        return f;

    }
}
