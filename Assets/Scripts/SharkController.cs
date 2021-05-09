using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public bool pursueingFish = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new SharkPath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fish" && pursueingFish == false)
        {
            GetComponent<Pursue>().target = other.GetComponent<FishBoid>();
            pursueingFish = true;
        }
    }

   
    
}

class SharkPath : State
{
    
    public override void Enter()
    {
        owner.GetComponent<FollowPath>().enabled = true;
    }

    public override void Think()
    {
        if (owner.GetComponent<SharkController>().pursueingFish)
        {
            owner.ChangeState(new PursueFish());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<FollowPath>().enabled = false;
    }
}

class PursueFish : State
{
    
    public override void Enter()
    {
        owner.GetComponent<Pursue>().enabled = true;
        owner.GetComponent<FishBoid>().maxSpeed = 10;
        owner.GetComponent<ObstacleAvoidance>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.GetComponent<Pursue>().target.transform.position,owner.transform.position) > 20f)
        {
            owner.ChangeState(new SharkPath());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<SharkController>().pursueingFish = false;
        owner.GetComponent<Pursue>().target = null;
        owner.GetComponent<Pursue>().enabled = false;
        owner.GetComponent<FishBoid>().maxSpeed = 4;
        owner.GetComponent<ObstacleAvoidance>().enabled = false;
    }
}

