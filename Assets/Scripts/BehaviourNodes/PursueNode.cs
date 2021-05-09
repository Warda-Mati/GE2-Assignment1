using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueNode : Node
{
    public DiverController diver;
    public FishBoid targetFish;
    public int range = 10;

    private bool addPursue = false;
    public PursueNode(DiverController diver, FishBoid targetFish)
    {
        this.diver = diver;
        this.targetFish = targetFish;
    }

    public override NodeState Evaluate()
    {
        if(_state == NodeState.SUCCESS)
            return NodeState.SUCCESS;
        
        
        if (GameObject.FindGameObjectsWithTag("dead").Length > 0)
        {
            addPursue = false;
            diver.GetComponent<Pursue>().enabled = false;
            diver.CancelInvoke();
            diver.GetComponent<FishBoid>().enabled = false;
            _state = NodeState.SUCCESS;
            return NodeState.SUCCESS;   
        }
        
        if (!addPursue)
        {
            diver.gameObject.GetComponent<Pursue>().enabled = true;
            FishBoid[] fishes = GameObject.FindObjectsOfType<FishBoid>();
            diver.GetComponent<Pursue>().target = fishes[0];
            diver.GetComponent<FishBoid>().maxSpeed = 5;
            diver.InvokeRepeating("ShootHarpoons",5,2);
            addPursue = true;
        }

         
        return NodeState.RUNNING;
        
    }
}
