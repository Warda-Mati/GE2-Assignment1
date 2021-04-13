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
            owner.transform.position) > 40)
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

class FollowPathState : State
{
    public override void Enter()
    {
       
        owner.GetComponent<FollowPath>().enabled = true;
    }

    public override void Think()
    {
        if (owner.GetComponent<DolphinController>().goingToDive)
        {
            owner.ChangeState(new MovetoDive());
        }
    }

    public override void Exit()
    {
        
        owner.GetComponent<FollowPath>().enabled = false;
    }
}

class MovetoDive : State
{
    public override void Enter()
    {
        owner.GetComponent<MoveToLand>().enabled = true;
    }

    public override void Think()
    {
        if (owner.GetComponent<DolphinController>().diving)
        {
            owner.ChangeState(new DiveState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<MoveToLand>().enabled = false;
    }
}

class DiveState : State
{
    public override void Enter()
    {
        owner.GetComponent<DolphinFlip>().enabled = true;
    }

    public override void Think()
    {
        if (owner.GetComponent<DolphinController>().splashesCounter == 2)
        {
            owner.ChangeState(new FollowPathState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<DolphinFlip>().enabled = false;
    }
}

