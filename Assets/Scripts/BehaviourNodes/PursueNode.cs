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
        Debug.Log("Purseing");
        if (targetFish == null)
            return NodeState.FAILURE;
        
        
        if (!addPursue)
        {
            diver.gameObject.GetComponent<Pursue>().enabled = true;
            diver.GetComponent<Pursue>().target = targetFish;
            diver.GetComponent<FishBoid>().maxSpeed = 5;
            addPursue = true;
        }

        if (Vector3.Distance(diver.transform.position, targetFish.transform.position) < range)
        {
            addPursue = false;
            diver.GetComponent<Pursue>().enabled = false;
            return NodeState.SUCCESS;   
        }
        else 
        {
            return NodeState.RUNNING;
        }
    }
}
