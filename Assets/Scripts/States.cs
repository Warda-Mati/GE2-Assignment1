using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class MoveState : State
{
    
    public override void Enter()
    {
        owner.GetComponent<jitterWander>().enabled = true;
        owner.GetComponent<Flee>().enabled = false;
        owner.GetComponent<FishController>().withinShark = false;
    }

    public override void Think()
    {
        if (owner.GetComponent<FishController>().withinShark)
        {
            owner.ChangeState(new FleeState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<jitterWander>().enabled = false;
    }
}

class FleeState : State
{
    public override void Enter()
    {
       
        owner.GetComponent<Flee>().enabled = true;
        owner.GetComponent<Flee>().targetGameObject = owner.GetComponent<FishController>().shark;
        owner.GetComponent<FlockingBehaviour>().enabled = false;
        owner.GetComponent<jitterWander>().enabled = false;

    }

    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<Flee>().targetGameObject.transform.position,
            owner.transform.position) > 20)
        {
            owner.ChangeState(new MoveState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Flee>().enabled = false;
        owner.GetComponent<FlockingBehaviour>().enabled = true;
    }
}
