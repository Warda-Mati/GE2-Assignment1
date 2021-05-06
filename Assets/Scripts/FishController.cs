using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public GameObject shark;
    public bool withinShark;
    
    public void OnTriggerEnter(Collider c)
    {
        if (c.tag == "shark")
        {
            withinShark = true;
        }

        if (c.tag == "harpoon")
        {
            GetComponent<StateMachine>().ChangeState(new Dead());
            GetComponent<StateMachine>().SetGlobalState(new Dead());
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        shark = GameObject.FindWithTag("shark");
        withinShark = false;
        GetComponent<StateMachine>().ChangeState(new MoveState());
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}


class Dead : State
{
    public override void Enter()
    {
        SteeringBehavior[] sbs = owner.GetComponent<FishBoid>().GetComponents<SteeringBehavior>();
        foreach(SteeringBehavior sb in sbs)
        {
            sb.enabled = false;
        }
        owner.GetComponent<StateMachine>().enabled = false;
        owner.tag = "dead";
    }     
}

