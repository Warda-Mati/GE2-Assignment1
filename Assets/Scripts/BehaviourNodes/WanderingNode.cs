using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingNode : Node
{
    private DiverController diver;

    public WanderingNode(DiverController diver)
    { 
        this.diver = diver;
    }
    public override NodeState Evaluate()
    {
        if (diver.isNearFish)
        {
            GameObject.Destroy(diver.GetComponent<NoiseWander>());
            Debug.Log("successs");
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.RUNNING;
        }
    }
}
