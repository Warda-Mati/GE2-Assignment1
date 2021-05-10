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
        if(_state == NodeState.SUCCESS)
            return NodeState.SUCCESS;
        
      
        if (!beginShooting)
        {
            diver.InvokeRepeating("ShootHarpoons",5,2);
            beginShooting = true;
        }

        if (GameObject.FindGameObjectsWithTag("dead").Length > 0)
        {
            if(_state == NodeState.SUCCESS)
            _state = NodeState.SUCCESS;
            return NodeState.SUCCESS;   
        }

        return NodeState.RUNNING;
    }
    
  
}
