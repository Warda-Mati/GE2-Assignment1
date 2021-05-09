using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingNode : Node
{
    
    private DiverController diver;
    private bool addWander = false;
    public WanderingNode(DiverController diver)
    { 
        this.diver = diver;
    }
    public override NodeState Evaluate()
    {
        if(_state == NodeState.SUCCESS)
            return NodeState.SUCCESS;
            
        if (!addWander)
        {
            diver.GetComponent<NoiseWander>().enabled = true;
            addWander = true;
        }
        if (diver.isNearFish)
        {
            
            diver.GetComponent<NoiseWander>().enabled = false;
            addWander = false;
            _state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
        return NodeState.RUNNING;
        
    }
}
