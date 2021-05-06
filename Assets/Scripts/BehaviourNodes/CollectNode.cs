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
        deadFish = GameObject.FindGameObjectsWithTag("dead");
        if (deadFish.Length > 0 && (diverController.fishCollected < diverController.maxFish))
        {
            Seek diverSeek = diverController.GetComponent<Seek>();
            diverSeek.enabled = true;
            diverSeek.targetGameObject = deadFish[0];
            GameObject targetFish = diverSeek.targetGameObject;
            if (Vector3.Distance(targetFish.transform.position, diverController.transform.position) < 1)
            {
                targetFish.transform.parent = diverController.tank.transform;
                targetFish.transform.localPosition = Vector3.zero;
                targetFish.tag = "collected";
                diverController.fishCollected += 1;

                return NodeState.SUCCESS;
            }
            else
            {
                return NodeState.RUNNING;
            }
            
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
