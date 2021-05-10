using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PirateShipController : MonoBehaviour
{

    public bool piratesNearby = false;
    public int health;

    public ParticleSystem explosion;
    
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
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "pirates")
        {
            if (other.gameObject.GetComponent<PirateShipController>().health < 0)
            {
                Debug.Log("enemy pirate defeated");
                piratesNearby = false;
            }
        }
    }
    
    
    
    

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GetComponent<StateMachine>().ChangeState(new ShipSink());
        }
    }

    void DestroyShip()
    {
        explosion.Play();
        Destroy(this.gameObject);
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
        owner.GetComponent<MoveToAttack>().enabled = true;
    }

    public override void Think()
    {
        
        
        if (pirate.piratesNearby == false)
        {
            Debug.Log("changing state to ship moving");
            owner.ChangeState(new ShipMoving());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<MoveToAttack>().enabled = false;
        owner.GetComponent<FireCannon>().enabled = false;
    }
}

class ShipSink : State
{
   
    public override void Enter()
    {
        owner.tag = "sinking";
    }

    public override void Think()
    {
        owner.GetComponent<DolphinFlip>().enabled = true;
        owner.GetComponent<PirateShipController>().Invoke("DestroyShip",16);
    }

    public override void Exit()
    {
        
    }
}

