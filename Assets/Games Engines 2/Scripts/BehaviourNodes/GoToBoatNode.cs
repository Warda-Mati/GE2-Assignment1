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
            GameObject caughtFish = diver.tank.transform.GetChild(0).gameObject;
            caughtFish.transform.localPosition = Vector3.zero;
            diver.GetComponent<Seek>().targetGameObject = boat;
            if (Vector3.Distance(diver.transform.position, boat.transform.position) < 5)
            {
                
                caughtFish.transform.parent = null;
                List<GameObject> fishes = caughtFish.GetComponent<FlockingBehaviour>().agents;
                for (int i = 0; i < fishes.Count; i++)
                {
                    GameObject entity = fishes[i];
                    if (entity == caughtFish)
                    {
                        fishes.Remove(caughtFish);
                    }
                }
                GameObject.Destroy(caughtFish);
                diver.fishCollected = false;
                diver.GetComponent<Seek>().targetGameObject = null;
                _state = NodeState.SUCCESS;
                return NodeState.SUCCESS;
            }

        }
        return NodeState.RUNNING;

    }
}
