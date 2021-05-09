using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBoatNode : Node
{
    public DiverController diver;
    public GameObject boat;

    public GoToBoatNode(DiverController diver, GameObject boat)
    {
        this.diver = diver;
        this.boat = boat;
    }
    public override NodeState Evaluate()
    {
        if (_state == NodeState.SUCCESS)
        {
            return NodeState.SUCCESS;
        }
  
        if (diver.fishCollected)
        {
            diver.GetComponent<Seek>().targetGameObject = boat;
            if (Vector3.Distance(diver.transform.position, boat.transform.position) < 1)
            {
                GameObject caughtFish = diver.tank.transform.GetChild(0).gameObject;
                caughtFish.transform.parent = null;
                GameObject.Destroy(caughtFish);
                diver.fishCollected = false;
                diver.GetComponent<Seek>().targetGameObject = null;
                return NodeState.SUCCESS;
            }

        }
        return NodeState.RUNNING;

    }
}
