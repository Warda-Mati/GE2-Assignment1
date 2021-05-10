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
        Debug.Log("Distance" + Vector3.Distance(diver.transform.position, diver.startPos));
        if (Vector3.Distance(diver.transform.position, diver.startPos) < 5)
        {
            Debug.Log("returned");
            diver.GetComponent<Seek>().enabled = false;
            //diver.GetComponent<FishBoid>().acceleration = Vector3.zero;
            //diver.GetComponent<FishBoid>().force = Vector3.zero;
            //diver.GetComponent<FishBoid>().velocity = Vector3.zero;
            diver.Invoke("RevertStates",1);
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }
}
