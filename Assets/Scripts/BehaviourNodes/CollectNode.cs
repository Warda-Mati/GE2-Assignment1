using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectNode : Node
{
    public DiverController diverController;
    private GameObject[] deadFish;

    public CollectNode(DiverController diver)
    {
        this.diverController = diver;
    }

    public override NodeState Evaluate()
    {
        if(_state == NodeState.SUCCESS)
            return NodeState.SUCCESS;
        
        deadFish = GameObject.FindGameObjectsWithTag("dead");
        if (deadFish.Length > 0 && !diverController.fishCollected)
        {

            Seek diverSeek = diverController.GetComponent<Seek>();
            diverSeek.enabled = true;
            diverController.GetComponent<FishBoid>().enabled = true;
            diverSeek.targetGameObject = deadFish[0];
            GameObject targetFish = diverSeek.targetGameObject;
            if (Vector3.Distance(targetFish.transform.position, diverController.transform.position) < 2)
            {
                targetFish.transform.parent = diverController.tank.transform;
                targetFish.transform.localPosition = Vector3.zero;
                targetFish.tag = "collected";
                diverController.fishCollected = true;
                diverSeek.targetGameObject = null;
                _state = NodeState.SUCCESS;
                return NodeState.SUCCESS;
            }

        }

        return NodeState.RUNNING;
    }
}
