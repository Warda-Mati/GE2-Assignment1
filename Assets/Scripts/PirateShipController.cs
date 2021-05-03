using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PirateShipController : MonoBehaviour
{

    public bool piratesNearby = false;
    public int health;
    
    private void Awake()
    {
        GetComponent<StateMachine>().ChangeState(new ShipMoving());
    }
    
    // Start is called before the first frame update
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pirates")
        {
            piratesNearby = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


// pirate ship
class ShipMoving : State
{
    public override void Enter()
    {
        owner.GetComponent<NoiseWander>().enabled = true;
    }

    public override void Think()
    {
        if (owner.GetComponent<PirateShipController>().piratesNearby)
        {
            owner.ChangeState(new AttackShip());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<NoiseWander>().enabled = false;
    }
}

// pirate ship
class AttackShip : State
{
    private PirateShipController pirate;
    public override void Enter()
    {
        //owner.GetComponent<MoveToAttack>().enabled = true;
        pirate = owner.GetComponent<PirateShipController>();
    }

    public override void Think()
    {
        owner.GetComponent<MoveToAttack>().enabled = true;
    }

    public override void Exit()
    {
        owner.GetComponent<MoveToAttack>().enabled = false;
    }
}
