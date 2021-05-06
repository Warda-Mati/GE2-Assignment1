using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNode : Node
{
    public DiverController diver;
    
    public float rotSpeed = 0.5f;
    public FishBoid targetFish;

    private bool beginShooting;
    public ShootNode(DiverController diver)
    {
        this.diver = diver;
    }

    public override NodeState Evaluate()
    {
        Vector3 toFish = targetFish.transform.position - diver.transform.position;
        diver.transform.rotation = Quaternion.RotateTowards(diver.transform.rotation,
            Quaternion.LookRotation(toFish)
            , rotSpeed * Time.deltaTime
        );
        if (!beginShooting)
        {
            diver.InvokeRepeating("ShootHarpoons",5,2);
            beginShooting = true;
        }

        if (targetFish.gameObject.tag == "dead")
        {
            //beginShooting = false;
            return NodeState.SUCCESS;   
        }

        return NodeState.RUNNING;
    }
    
  
}
