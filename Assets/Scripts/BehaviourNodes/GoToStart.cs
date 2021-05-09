using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToStart : Node
{
    public DiverController diver;

    public GoToStart(DiverController diver)
    {
        this.diver = diver;
    }
    public override NodeState Evaluate()
    {
        
        diver.GetComponent<Seek>().target = diver.startPos;
        if (Vector3.Distance(diver.transform.position, diver.startPos) < 1)
        {
           
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }
}
