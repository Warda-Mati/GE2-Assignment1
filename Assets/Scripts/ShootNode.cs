using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNode : Node
{
    public DiverController diver;
  

    public float rotSpeed;
    public FishBoid targetFish;

    public ShootNode(DiverController diver)
    {
        this.diver = diver;
    }

    public override NodeState Evaluate()
    {
        Vector3 toFish = targetFish.transform.position - diver.transform.position;
        diver.transform.localRotation = Quaternion.RotateTowards(diver.transform.rotation,
            Quaternion.LookRotation(toFish)
            , rotSpeed * Time.deltaTime
        );
        diver.InvokeRepeating("ShootHarpoons",5,5);
        if (targetFish.gameObject.tag == "dead")
            return NodeState.SUCCESS;
        else 
            return NodeState.RUNNING;
    }


  
}
