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
        
        if (!addPursue)
        {
            diver.gameObject.GetComponent<Pursue>().enabled = true;
            diver.GetComponent<Pursue>().target = targetFish;
            diver.GetComponent<FishBoid>().maxSpeed = 5;
            diver.InvokeRepeating("ShootHarpoons",5,2);
            addPursue = true;
        }

       // if (Vector3.Distance(diver.transform.position, targetFish.transform.position) < range)
        //{
        if (GameObject.FindGameObjectsWithTag("dead").Length > 0)
        {
            addPursue = false;
            diver.GetComponent<Pursue>().enabled = false;
            diver.CancelInvoke();
            _state = NodeState.SUCCESS;
            return NodeState.SUCCESS;   
        }
         
        return NodeState.RUNNING;
        
    }
}
