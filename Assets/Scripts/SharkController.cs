using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
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
        if (other.tag == "fish" && GetComponent<Pursue>().target != null)
        {

            GetComponent<FollowPath>().enabled = true;
            GetComponent<Pursue>().target = other.GetComponent<FishBoid>();
            GetComponent<StateMachine>().ChangeState(new PursueFish());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "fish")
        {
            GetComponent<Pursue>().target = null;
            if(GetComponent<Pursue>().target) 
                GetComponent<StateMachine>().RevertToPreviousState();
            
        }
    }
}

class SharkPath : State
{
    
    public override void Enter()
    {
        
    }

    public override void Think()
    {
        
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
        owner.GetComponent<ObstacleAvoidance>().enabled = true;
    }

    public override void Think()
    {
        
    }

    public override void Exit()
    {
        owner.GetComponent<Pursue>().enabled = false;
        owner.GetComponent<ObstacleAvoidance>().enabled = false;
    }
}

